using webProductos.Domain.Entities;

namespace webProductos.Domain.Repository;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task DeleteAsync(Product product);
    Task<Product> AddProductAsync(Product product);
    Task<bool> SaveChangesAsync();
}