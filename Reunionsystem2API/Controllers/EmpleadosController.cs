using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using Reunionsystem2API.DTOs;
using ReunionsystemAPI.Models.Entities;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoResponseDto>>> GetEmpleados()
        {
            return await _context.Empleados
                .Select(e => new EmpleadoResponseDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Correo = e.Email, // Cambia e.Email por el nombre real de la propiedad en tu entidad si es diferente
                    Departamento = e.Departamento
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoResponseDto>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            return new EmpleadoResponseDto
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Correo = empleado.Email,
                Departamento = empleado.Departamento
            };
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoResponseDto>> PostEmpleado(EmpleadoCreateDto dto)
        {
            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Email = dto.Correo, // Mapeo del DTO hacia la entidad
                Departamento = dto.Departamento
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            var responseDto = new EmpleadoResponseDto
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Correo = empleado.Email,
                Departamento = empleado.Departamento
            };

            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, EmpleadoCreateDto dto)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            empleado.Nombre = dto.Nombre;
            empleado.Email = dto.Correo;
            empleado.Departamento = dto.Departamento;

            _context.Entry(empleado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}