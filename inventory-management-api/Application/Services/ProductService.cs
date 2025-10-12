using Application.DTOs;
using Application.Ports;
using Domain.Entities;

namespace Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task<IEnumerable<ProductoDto>> GetAllProductsAsync()
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

    public async Task<ProductoDto> GetProductByIdAsync(string id)
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

    public async Task<ProductoDto> CreateProductAsync(ProductoDto productDto)
    {
        if (productDto == null) throw new ArgumentNullException(nameof(productDto), "Error this data is required");
        try
        {
            var product = new Producto()
            {
                Nombre = productDto.Nombre,
                Descripcion = productDto.Descripcion,
                Categoria = productDto.Categoria,
                Imagen = productDto.Imagen,
                Precio = productDto.Precio,
                Stock = productDto.Stock,
                Estado = productDto.Estado,
                FechaCreacion = productDto.FechaCreacion,
                UsuarioCreacion = productDto.UsuarioCreacion,
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

    public async Task<ProductoDto> UpdateProductAsync(string id, ProductoDto productDto)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        if (productDto == null) throw new ArgumentNullException(nameof(productDto), "Error this data is required");

        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            product.Nombre = productDto.Nombre;
            product.Descripcion = productDto.Descripcion;
            product.Categoria = productDto.Categoria;
            product.Imagen = productDto.Imagen;
            product.Precio = productDto.Precio;
            product.Stock = productDto.Stock;
            product.Estado = productDto.Estado;
            product.FechaCreacion = productDto.FechaCreacion;
            product.UsuarioCreacion = productDto.UsuarioCreacion;
            product.FechaModificacion = productDto.FechaModificacion;
            product.UsuarioModificacion = productDto.UsuarioModificacion;

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

    private static ProductoDto MapToDto(Producto product)
    {
        var response = new ProductoDto()
        {
            Id = product.Id.ToString(),
            Codigo = product.Codigo,
            Nombre = product.Nombre,
            Descripcion = product.Descripcion ?? string.Empty,
            Categoria = product.Categoria ?? string.Empty,
            Imagen = product.Imagen ?? string.Empty,
            Precio = product.Precio,
            Stock = product.Stock,
            Estado = product.Estado,
            FechaCreacion = product.FechaCreacion,
            UsuarioCreacion = product.UsuarioCreacion,
            FechaModificacion = product.FechaModificacion,
            UsuarioModificacion = product.UsuarioModificacion ?? string.Empty,
        };
        return response;
    }
}