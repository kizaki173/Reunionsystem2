using Microsoft.EntityFrameworkCore;
using ReunionsystemAPI.Models.Entities;
using System.Collections.Generic;

namespace Reunionsystem2API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
