using Application.DTOs;

namespace Application.Services;

public interface ITransactionService
{
 Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
 Task<TransactionDto> GetTransactionByIdAsync(string id);
 Task<TransactionDto> CreateTransactionAsync(TransactionDto transactionDto);
 Task<TransactionDto> UpdateTransactionAsync(string id, TransactionDto transactionDto);
 Task<bool> DeleteTransactionAsync(string id);
}