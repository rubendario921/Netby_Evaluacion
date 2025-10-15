using Application.DTOs;
using Application.Ports;
using Domain.Entities;

namespace Application.Services;

public class TransactionService(ITransactionRepository repository) : ITransactionService
{
    private readonly ITransactionRepository _repository = repository;

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        try
        {
            var transaction = await _repository.GetAllAsync();
            return transaction.Select(MapToDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<TransactionDto> GetTransactionByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        try
        {
            var transaction = await _repository.GetByIdAsync(id);
            return transaction == null ? throw new Exception("Transaction not found") : MapToDto(transaction);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<TransactionDto> CreateTransactionAsync(TransactionDto transactionDto)
    {
        if (transactionDto == null)
            throw new ArgumentNullException(nameof(transactionDto), "Error this data is required");
        try
        {
            var transaction = new Transaction()
            {
                Date = transactionDto.Date,
                TransactionType = transactionDto.TransactionType,
                Amount = transactionDto.Amount,
                UnitPrice = transactionDto.UnitPrice,
                PriceTotal = transactionDto.PriceTotal,
                Detail = transactionDto.Detail,
                Status = true,
                ProductId = transactionDto.ProductId,
                CreationDate = transactionDto.CreationDate,
                UserCreation = transactionDto.UserCreation ?? "System Default",
            };
            var response = await _repository.CreateAsync(transaction);
            return response == null ? throw new Exception("Transaction not created") : MapToDto(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<TransactionDto> UpdateTransactionAsync(string id, TransactionDto transactionDto)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        if (transactionDto == null)
            throw new ArgumentNullException(nameof(transactionDto), "Error this data is required");
        try
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) throw new Exception("Transaction not found");

            transaction.Date = transactionDto.Date;
            transaction.TransactionType = transactionDto.TransactionType;
            transaction.Amount = transactionDto.Amount;
            transaction.UnitPrice = transactionDto.UnitPrice;
            transaction.PriceTotal = transactionDto.PriceTotal;
            transaction.Detail = transactionDto.Detail;
            transaction.Status = transactionDto.Status;
            transaction.ProductId = transactionDto.ProductId;
            transaction.ModificationDate = transactionDto.ModificationDate;
            transaction.UserModification = transactionDto.UserModification ?? "System Default";

            var response = await _repository.UpdateAsync(transaction);
            return response == null ? throw new Exception("Transaction not updated") : MapToDto(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteTransactionAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        try
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) throw new Exception("Transaction not found");
            return await _repository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static TransactionDto MapToDto(Transaction transaction)
    {
        var response = new TransactionDto()
        {
            Id = transaction.Id.ToString(),
            Date = transaction.Date,
            TransactionType = transaction.TransactionType,
            Amount = transaction.Amount,
            UnitPrice = transaction.UnitPrice,
            PriceTotal = transaction.PriceTotal,
            Detail = transaction.Detail ?? string.Empty,
            Status = transaction.Status,
            ProductId = transaction.ProductId,
            CreationDate = transaction.CreationDate,
            UserCreation = transaction.UserCreation ?? string.Empty,
            ModificationDate = transaction.ModificationDate,
            UserModification = transaction.UserModification ?? string.Empty,
            CancellationDate = transaction.CancellationDate,
            UserCancellation = transaction.UserCancellation ?? string.Empty,
        };
        return response;
    }
}