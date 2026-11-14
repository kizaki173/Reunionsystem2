using AutoMapper;
using Reunionsystem2API.Application.Contract;
using Reunionsystem2API.Application.DTOs;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;

namespace Reunionsystem2API.Application.Services
{
    public class SalaService : ISalaService
    {
        private readonly IRepository<Sala> _salaRepository;
        private readonly IMapper _mapper;

        public SalaService(IRepository<Sala> salaRepository, IMapper mapper)
        {
            _salaRepository = salaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalaResponseDto>> GetAllSalasAsync()
        {
            var salas = await _salaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SalaResponseDto>>(salas);
        }

        public async Task<SalaResponseDto?> GetSalaByIdAsync(int id)
        {
            var sala = await _salaRepository.GetByIdAsync(id);
            if (sala == null) return null;
            return _mapper.Map<SalaResponseDto>(sala);
        }

        public async Task<SalaResponseDto> CreateSalaAsync(SalaCreateDto salaCreateDto)
        {
            if (string.IsNullOrWhiteSpace(salaCreateDto.Nombre))
            {
                throw new ArgumentException("El nombre de la sala es obligatorio.");
            }

            var sala = _mapper.Map<Sala>(salaCreateDto);
            await _salaRepository.AddAsync(sala);
            await _salaRepository.SaveChangesAsync();

            return _mapper.Map<SalaResponseDto>(sala);
        }
    }
}
