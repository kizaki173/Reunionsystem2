using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReunionsystemAPI.Models.Entities
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [ForeignKey(nameof(EmpleadoId))]
        public Empleado? Empleado { get; set; }

        [Required]
        public int SalaId { get; set; }

        [ForeignKey(nameof(SalaId))]
        public Sala? Sala { get; set; }

        [Required]
        public int HorarioId { get; set; }

        [ForeignKey(nameof(HorarioId))]
        public Horario? Horario { get; set; }
    }
}