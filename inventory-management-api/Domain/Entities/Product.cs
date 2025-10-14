namespace Domain.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? Image { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public string UserCreation { get; set; } = null!;

    public DateTime? ModificationDate { get; set; }

    public string? UserModification { get; set; }

    public DateTime? DeletionDate { get; set; }

    public string? UserDeletion { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
