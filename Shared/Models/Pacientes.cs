using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Pacientes
{
    public int IdPaciente { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Cedula { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Sexo { get; set; }

    public string? TipoSangre { get; set; }

    public string? ContactoEmergencia { get; set; }

    public string? TelefonoEmergencia { get; set; }

    public string? Alergias { get; set; }

    public string? Observaciones { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<ArchivosPacientes> ArchivosPacientes { get; set; } = new List<ArchivosPacientes>();

    public virtual ICollection<Citas> Citas { get; set; } = new List<Citas>();

    public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();

    public virtual ICollection<HistorialClinico> HistorialClinico { get; set; } = new List<HistorialClinico>();

    public virtual ICollection<Odontogramas> Odontogramas { get; set; } = new List<Odontogramas>();
}
