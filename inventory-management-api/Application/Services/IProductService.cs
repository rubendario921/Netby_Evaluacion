using Application.DTOs;

namespace Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductoDto>> GetAllProductsAsync();
    Task<ProductoDto> GetProductByIdAsync(string id);
    Task<ProductoDto> CreateProductAsync(ProductoDto productDto);
    Task<ProductoDto> UpdateProductAsync(string id, ProductoDto productDto);
    Task<bool> DeleteProductAsync(string id);
}


