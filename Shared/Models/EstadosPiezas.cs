using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class EstadosPiezas
{
    public int IdEstadoPieza { get; set; }

    public int IdOdontograma { get; set; }

    public int IdPieza { get; set; }

    public string Estado { get; set; } = null!;

    public string? Observaciones { get; set; }

    public virtual Odontogramas? IdOdontogramaNavigation { get; set; } = null!;

    public virtual PiezasDentales? IdPiezaNavigation { get; set; } = null!;

    public virtual ICollection<TratamientosPiezas>? TratamientosPiezas { get; set; } = new List<TratamientosPiezas>();
}
