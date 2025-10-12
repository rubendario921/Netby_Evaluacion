namespace Domain.Entities;

public partial class Transaccione
{
    public Guid Id { get; set; }

    public DateTime Fecha { get; set; }

    public string TipoTransaccion { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal PrecioTotal { get; set; }

    public string? Detalle { get; set; }

    public bool Estado { get; set; }

    public Guid ProductoId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaAnulacion { get; set; }

    public string? UsuarioAnulacion { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
