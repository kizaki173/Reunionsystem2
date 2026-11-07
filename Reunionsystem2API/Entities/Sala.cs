using System.ComponentModel.DataAnnotations;

namespace ReunionsystemAPI.Models.Entities
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Capacidad { get; set; }

        public string? Ubicacion { get; set; }

        public bool Estado { get; set; } = true;

        // Propiedad de navegación
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}