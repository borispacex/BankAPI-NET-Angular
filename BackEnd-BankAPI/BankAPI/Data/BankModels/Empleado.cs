﻿using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? NombreCompleto { get; set; }
        public int? IdDepartamento { get; set; }
        public decimal? Sueldo { get; set; }
        public DateTime? FechaContrato { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
    }
}
