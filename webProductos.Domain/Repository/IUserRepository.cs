using webProductos.Domain.Entities;

namespace webProductos.Domain.Repository;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task DeleteAsync(User product);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddUserAsync(User user);
    Task<bool> SaveChangesAsync();
}