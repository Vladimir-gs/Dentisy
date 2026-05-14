using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Telefono { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Citas>? Citas { get; set; } = new List<Citas>();

    public virtual ICollection<HistorialClinico>? HistorialClinico { get; set; } = new List<HistorialClinico>();

    public virtual Roles? IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Odontogramas>? Odontogramas { get; set; } = new List<Odontogramas>();
}
