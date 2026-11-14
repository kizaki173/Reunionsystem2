using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using Reunionsystem2API.Domain.Entities;
using Reunionsystem2API.Domain.Interfaces;

namespace Reunionsystem2API.Infrastructure.Repositories
{
    public class ReservaRepository : Repository<Reserva>, IReservaRepository
    {

        public ReservaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reserva>> GetReservasConDetallesAsync()
        {
          
            return await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Empleado)
                .Include(r => r.Horario)
                .ToListAsync();
        }
    }
}