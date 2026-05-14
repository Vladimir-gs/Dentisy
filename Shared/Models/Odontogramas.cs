using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Odontogramas
{
    public int IdOdontograma { get; set; }

    public int IdPaciente { get; set; }

    public int IdDoctor { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<EstadosPiezas>? EstadosPiezas { get; set; } = new List<EstadosPiezas>();

    public virtual Usuarios? IdDoctorNavigation { get; set; } = null!;

    public virtual Pacientes? IdPacienteNavigation { get; set; } = null!;
}
