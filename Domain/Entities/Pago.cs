using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Pago
{
    public int CodigoCliente { get; set; }

    public string FormaPago { get; set; } = null!;

    public string IdTransacion { get; set; } = null!;

    public DateOnly FechaPago { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
