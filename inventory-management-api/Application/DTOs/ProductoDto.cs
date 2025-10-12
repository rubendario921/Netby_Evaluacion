namespace Application.DTOs;

public class ProductoDto
{
    public string Id { get; set; } = string.Empty;
    public required string Codigo { get; set; }
    public required string Nombre { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
    public required decimal Precio { get; set; }
    public int Stock { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string UsuarioCreacion { get; set; } = string.Empty;
    public DateTime? FechaModificacion { get; set; }
    public string UsuarioModificacion { get; set; } = string.Empty;
}