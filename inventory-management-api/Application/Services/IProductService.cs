using Application.DTOs;

namespace Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(string id);
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task<ProductDto> UpdateProductAsync(string id, ProductDto productDto);
    Task<bool> DeleteProductAsync(string id);
}


