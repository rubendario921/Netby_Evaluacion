using Domain.Entities;

namespace Application.Ports;

public interface IProductRepository
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(string id);
    Task<Producto> CreateAsync(Producto product);
    Task<Producto> UpdateAsync(Producto product);
    Task<bool> DeleteAsync(string id);
}