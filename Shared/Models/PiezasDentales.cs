using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class PiezasDentales
{
    public int IdPieza { get; set; }

    public string Numero { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<EstadosPiezas> EstadosPiezas { get; set; } = new List<EstadosPiezas>();
}
