namespace Reunionsystem2API.Application.DTOs
{
    public class EmpleadoCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
    }

    public class EmpleadoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
    }
}