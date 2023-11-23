namespace API.Dtos;

public partial class EmpleadoDto
{
    public int CodigoEmpleado { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido1 { get; set; } = null!;
    public string Apellido2 { get; set; }
    public string Extension { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CodigoOficina { get; set; } = null!;
    public int CodigoJefe { get; set; }
    public string Puesto { get; set; }
}
