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
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservas()
        {
            var reservas = await _reservaService.GetAllReservasAsync();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReserva(int id)
        {
            var reserva = await _reservaService.GetReservaByIdAsync(id);
            if (reserva == null) return NotFound();
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> PostReserva([FromBody] ReservaCreateDto reservaCreateDto)
        {
            try
            {
                var nuevaReserva = await _reservaService.CreateReservaAsync(reservaCreateDto);
                return CreatedAtAction(nameof(GetReserva), new { id = nuevaReserva.Id }, nuevaReserva);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}