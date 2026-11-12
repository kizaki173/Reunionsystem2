using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.DTOs;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IRepository<Empleado> _empleadoRepository;
        private readonly IMapper _mapper;

        public EmpleadosController(IRepository<Empleado> empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoResponseDto>>> GetEmpleados()
        {
            var empleados = await _empleadoRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EmpleadoResponseDto>>(empleados));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoResponseDto>> GetEmpleado(int id)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmpleadoResponseDto>(empleado);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoResponseDto>> PostEmpleado(EmpleadoCreateDto empleadoCreateDto)
        {
            var empleado = _mapper.Map<Empleado>(empleadoCreateDto);

            await _empleadoRepository.AddAsync(empleado);
            await _empleadoRepository.SaveChangesAsync();

            var empleadoResponse = _mapper.Map<EmpleadoResponseDto>(empleado);

            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleadoResponse);
        }
    }
}