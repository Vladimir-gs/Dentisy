using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class HistorialClinico
{
    public int IdHistorial { get; set; }

    public int IdPaciente { get; set; }

    public int IdDoctor { get; set; }

    public DateTime? Fecha { get; set; }

    public string? ObservacionesGenerales { get; set; }

    public virtual ICollection<HistorialTratamientos> HistorialTratamientos { get; set; } = new List<HistorialTratamientos>();

    public virtual Usuarios IdDoctorNavigation { get; set; } = null!;

    public virtual Pacientes IdPacienteNavigation { get; set; } = null!;
}
