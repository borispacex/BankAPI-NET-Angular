using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankAPI.Data.BankModels
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdDepartamento { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
