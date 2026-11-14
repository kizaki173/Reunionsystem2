using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reunionsystem2API.Application.DTOs;

namespace Reunionsystem2API.Application.Contract
{
    public interface ISalaService
    {
        Task<IEnumerable<SalaResponseDto>> GetAllSalasAsync();
        Task<SalaResponseDto?> GetSalaByIdAsync(int id);
        Task<SalaResponseDto> CreateSalaAsync(SalaCreateDto salaCreateDto);
    }
}
