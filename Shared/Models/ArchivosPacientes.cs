using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class ArchivosPacientes
{
    public int IdArchivo { get; set; }

    public int IdPaciente { get; set; }

    public string Url { get; set; } = null!;

    public string? Tipo { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Pacientes IdPacienteNavigation { get; set; } = null!;
}
