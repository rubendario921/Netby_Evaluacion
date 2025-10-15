using Application.Ports;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository(ApplicationDbContext context) : ITransactionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        try
        {
            return await _context.Transactions.Where(t => t.Status).OrderByDescending(t => t.CreationDate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Transaction?> GetByIdAsync(string id)
    {
        try
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction), "Error this data is required");
        try
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction), "Error this data is required");
        try
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transaction;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), "Error this data is required");
        try
        {
            var transaction = await GetByIdAsync(id);
            if (transaction == null)
                return false;
            transaction.Status = false;
            await UpdateAsync(transaction);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}