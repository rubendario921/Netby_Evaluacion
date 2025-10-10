namespace inventory_management_api.Domain.Entities;

public partial class Producto
{
    public Guid Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Categoria { get; set; }

    public string? Imagen { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaEliminacion { get; set; }

    public string? UsuarioEliminacion { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}