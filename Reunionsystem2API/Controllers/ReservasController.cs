using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.DTOs;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IRepository<Reserva> _reservaRepository;
        private readonly IMapper _mapper;

        public ReservasController(IRepository<Reserva> reservaRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaResponseDto>>> GetReservas()
        {
            var reservas = await _reservaRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ReservaResponseDto>>(reservas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaResponseDto>> GetReserva(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReservaResponseDto>(reserva);
        }

        [HttpPost]
        public async Task<ActionResult<ReservaResponseDto>> PostReserva(ReservaCreateDto reservaCreateDto)
        {
            var reserva = _mapper.Map<Reserva>(reservaCreateDto);

            await _reservaRepository.AddAsync(reserva);
            await _reservaRepository.SaveChangesAsync();

            var reservaResponse = _mapper.Map<ReservaResponseDto>(reserva);

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reservaResponse);
        }
    }
}