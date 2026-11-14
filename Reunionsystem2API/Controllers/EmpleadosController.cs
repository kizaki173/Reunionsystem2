using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.Application.DTOs;
using Reunionsystem2API.Application.Contract;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IRepository<Empleado> _empleadoRepository;

        public EmpleadosController(IRepository<Empleado> empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            var empleados = await _empleadoRepository.GetAllAsync();
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleado(int id)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(id);
            if (empleado == null) return NotFound();
            return Ok(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> PostEmpleado([FromBody] EmpleadoCreateDto empleadoCreateDto)
        {
            var empleado = new Empleado
            {
                Nombre = empleadoCreateDto.Nombre,
                Email = empleadoCreateDto.Correo,
                Departamento = empleadoCreateDto.Departamento
            };

            await _empleadoRepository.AddAsync(empleado);
            await _empleadoRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleado);
        }
    }
}