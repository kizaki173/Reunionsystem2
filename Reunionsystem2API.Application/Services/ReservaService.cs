using AutoMapper;
using Reunionsystem2API.Application.Contract;
using Reunionsystem2API.Application.DTOs;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;

namespace Reunionsystem2API.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository reservaRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservaResponseDto>> GetAllReservasAsync()
        {
            var reservas = await _reservaRepository.GetReservasConDetallesAsync();
            return _mapper.Map<IEnumerable<ReservaResponseDto>>(reservas);
        }

        public async Task<ReservaResponseDto?> GetReservaByIdAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return null;
            return _mapper.Map<ReservaResponseDto>(reserva);
        }

        public async Task<ReservaResponseDto> CreateReservaAsync(ReservaCreateDto reservaCreateDto)
        {
            if (reservaCreateDto.SalaId <= 0 || reservaCreateDto.EmpleadoId <= 0)
            {
                throw new ArgumentException("Debe especificar una Sala y un Empleado válidos para la reserva.");
            }

            var reserva = _mapper.Map<Reserva>(reservaCreateDto);
            await _reservaRepository.AddAsync(reserva);
            await _reservaRepository.SaveChangesAsync();

            return _mapper.Map<ReservaResponseDto>(reserva);
        }
    }
}