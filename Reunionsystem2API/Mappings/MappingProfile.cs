using AutoMapper;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.DTOs;

namespace Reunionsystem2API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Empleado, EmpleadoResponseDto>().ReverseMap();
            CreateMap<EmpleadoCreateDto, Empleado>();

            CreateMap<Horario, HorarioResponseDto>().ReverseMap();
            CreateMap<HorarioCreateDto, Horario>();

            CreateMap<Sala, SalaResponseDto>().ReverseMap();
            CreateMap<SalaCreateDto, Sala>();

            CreateMap<Reserva, ReservaResponseDto>().ReverseMap();
            CreateMap<ReservaCreateDto, Reserva>();
        }
    }
}