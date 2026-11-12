using System.ComponentModel.DataAnnotations;

namespace Reunionsystem2API.Domain.Entities
{
    public class Horario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}