namespace Domain.Entities;

public partial class Transaction
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string TransactionType { get; set; } = null!;

    public int Amount { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal PriceTotal { get; set; }

    public string? Detail { get; set; }

    public bool Status { get; set; }

    public Guid ProductId { get; set; }

    public DateTime CreationDate { get; set; }

    public string UserCreation { get; set; } = null!;

    public DateTime? ModificationDate { get; set; }

    public string? UserModification { get; set; }

    public DateTime? CancellationDate { get; set; }

    public string? UserCancellation { get; set; }

    public virtual Product Product { get; set; } = null!;
}
