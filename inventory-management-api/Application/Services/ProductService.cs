using Application.DTOs;
using Application.Ports;
using Domain.Entities;

namespace Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        try
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ProductDto> GetProductByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        try
        {
            var response = await _repository.GetByIdAsync(id);
            return response == null ? throw new Exception("Product not found") : MapToDto(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        if (productDto == null) throw new ArgumentNullException(nameof(productDto), "Error this data is required");
        try
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Code = productDto.Code,
                Description = productDto.Description,
                Category = productDto.Category,
                Image = productDto.Image,
                Price = productDto.Price,
                Stock = productDto.Stock,
                Status = productDto.Status,
                CreationDate = productDto.CreationDate,
                UserCreation = productDto.UserCreation ?? "System Default",
            };
            var response = await _repository.CreateAsync(product);
            return response == null ? throw new Exception("Product not created") : MapToDto(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<ProductDto> UpdateProductAsync(string id, ProductDto productDto)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        if (productDto == null) throw new ArgumentNullException(nameof(productDto), "Error this data is required");

        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Category = productDto.Category;
            product.Image = productDto.Image;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.Status = productDto.Status;
            product.ModificationDate = productDto.ModificationDate;
            product.UserModification = productDto.UserModification ?? "System Default";

            var response = await _repository.UpdateAsync(product);
            return response == null ? throw new Exception("Product not updated") : MapToDto(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        return await _repository.DeleteAsync(id);
    }

    private static ProductDto MapToDto(Product product)
    {
        var response = new ProductDto()
        {
            Id = product.Id.ToString(),
            Code = product.Code,
            Name = product.Name,
            Description = product.Description ?? string.Empty,
            Category = product.Category ?? string.Empty,
            Image = product.Image ?? string.Empty,
            Price = product.Price,
            Stock = product.Stock,
            Status = product.Status,
            CreationDate = product.CreationDate,
            UserCreation = product.UserCreation,
            ModificationDate = product.ModificationDate,
            UserModification = product.UserModification ?? string.Empty,
        };
        return response;
    }
}