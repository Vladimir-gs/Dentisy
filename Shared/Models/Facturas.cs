using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Facturas
{
    public int IdFactura { get; set; }

    public string? NumeroFactura { get; set; }

    public int IdPaciente { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Itbis { get; set; }

    public decimal? Total { get; set; }

    public string? EstadoPago { get; set; }

    public string? MetodoPago { get; set; }

    public string? Comentarios { get; set; }

    public virtual ICollection<DetalleFactura>? DetalleFactura { get; set; } = new List<DetalleFactura>();

    public virtual Pacientes? IdPacienteNavigation { get; set; } = null!;
}
