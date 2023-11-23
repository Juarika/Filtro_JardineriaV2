namespace API.Dtos;

public partial class PagoDto
{
    public int CodigoCliente { get; set; }
    public string FormaPago { get; set; } = null!;
    public string IdTransacion { get; set; } = null!;
    public DateOnly FechaPago { get; set; }
    public decimal Total { get; set; }
}
