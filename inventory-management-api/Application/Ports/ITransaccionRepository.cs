using Domain.Entities;

namespace Application.Ports;

public interface ITransaccionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(int id);
    Task<Transaction> CreateAsync(Transaction transaccion);
    Task<Transaction> UpdateAsync(Product transaccion);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}