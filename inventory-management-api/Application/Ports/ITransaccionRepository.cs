using Domain.Entities;

namespace Application.Ports;

public interface ITransaccionRepository
{
    Task<IEnumerable<Transaccione>> GetAllAsync();
    Task<Transaccione?> GetByIdAsync(int id);
    Task<Transaccione> CreateAsync(Transaccione transaccion);
    Task<Transaccione> UpdateAsync(Producto transaccion);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}