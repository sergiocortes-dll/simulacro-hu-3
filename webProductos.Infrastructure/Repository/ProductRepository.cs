using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webProductos.Application.DTOs;
using webProductos.Domain.Entities;
using webProductos.Domain.Repository;
using webProductos.Infrastructure.Data;

namespace webProductos.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.products
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteAsync(Product product)
    {
        _context.products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _context.products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving changes to database");
            throw;
        }
    }
}