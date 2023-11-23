namespace API.Dtos;

public partial class GamaProductoDto
{
    public string Gama { get; set; } = null!;
    public string DescripcionTexto { get; set; }
    public string DescripcionHtml { get; set; }
    public string Imagen { get; set; }
}
