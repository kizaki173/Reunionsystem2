using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using Reunionsystem2API.DTOs;
using ReunionsystemAPI.Models.Entities;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaResponseDto>>> GetReservas()
        {
            return await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Empleado)
                .Select(r => new ReservaResponseDto
                {
                    Id = r.Id,
                    SalaId = r.SalaId,
                    SalaNombre = r.Sala != null ? r.Sala.Nombre : string.Empty,
                    EmpleadoId = r.EmpleadoId,
                    EmpleadoNombre = r.Empleado != null ? r.Empleado.Nombre : string.Empty,
                    HorarioId = r.HorarioId,
                    Fecha = r.Fecha
                })
                .ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaResponseDto>> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Empleado)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null) return NotFound();

            return new ReservaResponseDto
            {
                Id = reserva.Id,
                SalaId = reserva.SalaId,
                SalaNombre = reserva.Sala?.Nombre ?? string.Empty,
                EmpleadoId = reserva.EmpleadoId,
                EmpleadoNombre = reserva.Empleado?.Nombre ?? string.Empty,
                HorarioId = reserva.HorarioId,
                Fecha = reserva.Fecha
            };
        }

        // POST: api/Reservas
        [HttpPost]
        public async Task<ActionResult<ReservaResponseDto>> PostReserva(ReservaCreateDto dto)
        {
            var reserva = new Reserva
            {
                SalaId = dto.SalaId,
                EmpleadoId = dto.EmpleadoId,
                HorarioId = dto.HorarioId,
                Fecha = dto.Fecha
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            var reservaCreada = await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Empleado)
                .FirstAsync(r => r.Id == reserva.Id);

            var responseDto = new ReservaResponseDto
            {
                Id = reservaCreada.Id,
                SalaId = reservaCreada.SalaId,
                SalaNombre = reservaCreada.Sala?.Nombre ?? string.Empty,
                EmpleadoId = reservaCreada.EmpleadoId,
                EmpleadoNombre = reservaCreada.Empleado?.Nombre ?? string.Empty,
                HorarioId = reservaCreada.HorarioId,
                Fecha = reservaCreada.Fecha
            };

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, responseDto);
        }

        // PUT: api/Reservas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, ReservaCreateDto dto)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            reserva.SalaId = dto.SalaId;
            reserva.EmpleadoId = dto.EmpleadoId;
            reserva.HorarioId = dto.HorarioId;
            reserva.Fecha = dto.Fecha;

            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}