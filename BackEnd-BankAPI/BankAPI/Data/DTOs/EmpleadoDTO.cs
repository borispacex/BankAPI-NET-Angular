namespace BankAPI.Data.DTOs
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        public string? NombreCompleto { get; set; }
        public int? IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public decimal? Sueldo { get; set; }
        public string? FechaContrato { get; set; }
    }
}
