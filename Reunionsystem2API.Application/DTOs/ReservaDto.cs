namespace Reunionsystem2API.Application.DTOs
{
    public class ReservaCreateDto
    {
        public int SalaId { get; set; }
        public int EmpleadoId { get; set; }
        public int HorarioId { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class ReservaResponseDto
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public string SalaNombre { get; set; } = string.Empty;
        public int EmpleadoId { get; set; }
        public string EmpleadoNombre { get; set; } = string.Empty;
        public int HorarioId { get; set; }
        public DateTime Fecha { get; set; }
    }
}