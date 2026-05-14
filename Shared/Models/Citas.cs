using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Citas
{
    public int IdCita { get; set; }

    public int IdPaciente { get; set; }

    public int IdDoctor { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string? Estado { get; set; }

    public string? Motivo { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Usuarios IdDoctorNavigation { get; set; } = null!;

    public virtual Pacientes IdPacienteNavigation { get; set; } = null!;
}
