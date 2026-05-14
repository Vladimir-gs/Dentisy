using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class HistorialTratamientos
{
    public int IdHistorialTratamiento { get; set; }

    public int IdHistorial { get; set; }

    public int IdTratamiento { get; set; }

    public string? Observaciones { get; set; }

    public decimal? Precio { get; set; }

    public virtual HistorialClinico IdHistorialNavigation { get; set; } = null!;

    public virtual Tratamientos IdTratamientoNavigation { get; set; } = null!;
}
