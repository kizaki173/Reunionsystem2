using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using Reunionsystem2API.DTOs;
using ReunionsystemAPI.Models.Entities;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaResponseDto>>> GetSalas()
        {
            var salas = await _context.Salas
                .Select(s => new SalaResponseDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Capacidad = s.Capacidad,
                })
                .ToListAsync();

            return Ok(salas);
        }

        // GET: api/Salas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaResponseDto>> GetSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            var salaDto = new SalaResponseDto
            {
                Id = sala.Id,
                Nombre = sala.Nombre,
                Capacidad = sala.Capacidad,
            };

            return Ok(salaDto);
        }

        // POST: api/Salas
        [HttpPost]
        public async Task<ActionResult<SalaResponseDto>> PostSala(SalaCreateDto salaDto)
        {
            var sala = new Sala
            {
                Nombre = salaDto.Nombre,
                Capacidad = salaDto.Capacidad,
                Ubicacion = salaDto.Ubicacion
            };

            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            var responseDto = new SalaResponseDto
            {
                Id = sala.Id,
                Nombre = sala.Nombre,
                Capacidad = sala.Capacidad,
                Ubicacion = sala.Ubicacion
            };

            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, responseDto);
        }

        // PUT: api/Salas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, SalaCreateDto salaDto)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }

            sala.Nombre = salaDto.Nombre;
            sala.Capacidad = salaDto.Capacidad;
            sala.Ubicacion = salaDto.Ubicacion;

            _context.Entry(sala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Salas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Salas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
