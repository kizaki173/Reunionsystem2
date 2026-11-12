using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;

namespace Reunionsystem2API.Infrastructure.Repositories
{
    public class SalaRepository : Repository<Sala>, ISalaRepository
    {
        public SalaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Sala?> GetSalaByNombreAsync(string nombre)
        {
            return await _context.Salas
                .FirstOrDefaultAsync(s => s.Nombre.ToLower() == nombre.ToLower());
        }
    }
}