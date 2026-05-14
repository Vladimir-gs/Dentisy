using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Productos
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Codigo { get; set; }

    public int? StockActual { get; set; }

    public int? StockMinimo { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? PrecioVenta { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public bool? Estado { get; set; }
}
