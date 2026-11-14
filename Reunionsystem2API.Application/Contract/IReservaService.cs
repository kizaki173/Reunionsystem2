using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reunionsystem2API.Application.DTOs;

namespace Reunionsystem2API.Application.Contract
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaResponseDto>> GetAllReservasAsync();
        Task<ReservaResponseDto?> GetReservaByIdAsync(int id);
        Task<ReservaResponseDto> CreateReservaAsync(ReservaCreateDto reservaCreateDto);
    }
}
