using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using Reunionsystem2API.DTOs;
using ReunionsystemAPI.Models.Entities;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HorariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioResponseDto>>> GetHorarios()
        {
            return await _context.Horarios
                .Select(h => new HorarioResponseDto
                {
                    Id = h.Id,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin
                })
                .ToListAsync();
        }

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioResponseDto>> GetHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return NotFound();

            return new HorarioResponseDto
            {
                Id = horario.Id,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin
            };
        }

        // POST: api/Horarios
        [HttpPost]
        public async Task<ActionResult<HorarioResponseDto>> PostHorario(HorarioCreateDto dto)
        {
            var horario = new Horario
            {
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin
            };

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            var responseDto = new HorarioResponseDto
            {
                Id = horario.Id,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin
            };

            return CreatedAtAction(nameof(GetHorario), new { id = horario.Id }, responseDto);
        }

        // PUT: api/Horarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, HorarioCreateDto dto)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return NotFound();

            horario.HoraInicio = dto.HoraInicio;
            horario.HoraFin = dto.HoraFin;

            _context.Entry(horario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return NotFound();

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}