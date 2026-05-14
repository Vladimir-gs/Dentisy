using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class DetalleFactura
{
    public int IdDetalle { get; set; }

    public int IdFactura { get; set; }

    public int IdTratamiento { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Facturas IdFacturaNavigation { get; set; } = null!;

    public virtual Tratamientos IdTratamientoNavigation { get; set; } = null!;
}
