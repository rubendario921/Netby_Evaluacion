using Application.Ports;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        try
        {
            return await _context.Productos.Where(p => p.Estado).OrderByDescending(p => p.FechaCreacion).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Producto?> GetByIdAsync(string id)
    {
        try
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Producto> CreateAsync(Producto product)
    {
        try
        {
            _context.Productos.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Producto> UpdateAsync(Producto product)
    {
        try
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var product = await GetByIdAsync(id);
        if (product == null)
            return false;

        product.Estado = false;
        await UpdateAsync(product);
        return true;
    }
}