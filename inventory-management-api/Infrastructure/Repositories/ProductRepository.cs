using Application.Ports;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            return await _context.Products.Where(p => p.Status).OrderByDescending(p => p.CreationDate).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        try
        {
            var consultId = Guid.Parse(id);    
            return await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(consultId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Product> CreateAsync(Product product)
    {
        try
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Product> UpdateAsync(Product product)
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

        product.Status = false;
        await UpdateAsync(product);
        return true;
    }
}