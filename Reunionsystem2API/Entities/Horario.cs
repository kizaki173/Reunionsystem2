using System.ComponentModel.DataAnnotations;

namespace ReunionsystemAPI.Models.Entities
{
    public class Horario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        // Propiedad de navegación
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}