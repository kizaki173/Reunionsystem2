using System.ComponentModel.DataAnnotations;

namespace ReunionsystemAPI.Models.Entities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Departamento { get; set; } = string.Empty;

        // Propiedad de navegación
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
