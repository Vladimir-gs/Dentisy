using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class TratamientosPiezas
{
    public int IdTratamientoPieza { get; set; }

    public int IdEstadoPieza { get; set; }

    public int IdTratamiento { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Observaciones { get; set; }

    public virtual EstadosPiezas? IdEstadoPiezaNavigation { get; set; } = null!;

    public virtual Tratamientos? IdTratamientoNavigation { get; set; } = null!;
}
