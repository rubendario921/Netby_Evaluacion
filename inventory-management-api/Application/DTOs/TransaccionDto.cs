namespace Application.DTOs;

public class TransaccionDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string TipoTransaccion { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal PrecioTotal { get; set; }
    public string Detalle { get; set; } = string.Empty;   
    public bool Estado { get; set; }

    public Guid ProductoId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaAnulacion { get; set; }

    public string? UsuarioAnulacion { get; set; }
}