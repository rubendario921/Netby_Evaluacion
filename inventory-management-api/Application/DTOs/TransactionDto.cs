namespace Application.DTOs;

public class TransactionDto
{
    public string Id { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal PriceTotal { get; set; }
    public string Detail { get; set; } = string.Empty;   
    public bool Status { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreationDate { get; set; }
    public string UserCreation { get; set; } = string.Empty;
    public DateTime? ModificationDate { get; set; }
    public string? UserModification { get; set; }
    public DateTime? CancellationDate { get; set; }
    public string? UserCancellation { get; set; }
}