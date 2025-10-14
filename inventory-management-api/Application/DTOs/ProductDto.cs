namespace Application.DTOs;

public class ProductDto
{
    public string Id { get; set; } = string.Empty;
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public int Stock { get; set; }
    public bool Status { get; set; }
    public DateTime CreationDate { get; set; }
    public string UserCreation { get; set; } = string.Empty;
    public DateTime? ModificationDate { get; set; }
    public string UserModification { get; set; } = string.Empty;
    public DateTime? DeletionDate { get; set; }
    public string UserDeletion { get; set; } = string.Empty;
}