using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.DTOs;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IRepository<Horario> _horarioRepository;
        private readonly IMapper _mapper;

        public HorariosController(IRepository<Horario> horarioRepository, IMapper mapper)
        {
            _horarioRepository = horarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioResponseDto>>> GetHorarios()
        {
            var horarios = await _horarioRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<HorarioResponseDto>>(horarios));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioResponseDto>> GetHorario(int id)
        {
            var horario = await _horarioRepository.GetByIdAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return _mapper.Map<HorarioResponseDto>(horario);
        }

        [HttpPost]
        public async Task<ActionResult<HorarioResponseDto>> PostHorario(HorarioCreateDto horarioCreateDto)
        {
            var horario = _mapper.Map<Horario>(horarioCreateDto);

            await _horarioRepository.AddAsync(horario);
            await _horarioRepository.SaveChangesAsync();

            var horarioResponse = _mapper.Map<HorarioResponseDto>(horario);

            return CreatedAtAction(nameof(GetHorario), new { id = horario.Id }, horarioResponse);
        }
    }
}