using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Tratamientos
{
    public int IdTratamiento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? DuracionAproximada { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFactura { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<HistorialTratamientos> HistorialTratamientos { get; set; } = new List<HistorialTratamientos>();

    public virtual ICollection<TratamientosPiezas> TratamientosPiezas { get; set; } = new List<TratamientosPiezas>();
}
