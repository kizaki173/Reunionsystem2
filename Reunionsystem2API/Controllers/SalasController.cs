using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.DTOs;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly IRepository<Sala> _salaRepository;
        private readonly IMapper _mapper;

        public SalasController(IRepository<Sala> salaRepository, IMapper mapper)
        {
            _salaRepository = salaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaResponseDto>>> GetSalas()
        {
            var salas = await _salaRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SalaResponseDto>>(salas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaResponseDto>> GetSala(int id)
        {
            var sala = await _salaRepository.GetByIdAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            return _mapper.Map<SalaResponseDto>(sala);
        }

        [HttpPost]
        public async Task<ActionResult<SalaResponseDto>> PostSala(SalaCreateDto salaCreateDto)
        {
            var sala = _mapper.Map<Sala>(salaCreateDto);

            await _salaRepository.AddAsync(sala);
            await _salaRepository.SaveChangesAsync();

            var salaResponse = _mapper.Map<SalaResponseDto>(sala);

            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, salaResponse);
        }
    }
}