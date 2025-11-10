using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webProductos.Domain.Entities;
using webProductos.Domain.Repository;
using webProductos.Infrastructure.Data;

namespace webProductos.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await  _context.users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteAsync(User user)
    {
        _context.users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _context.users.FirstOrDefaultAsync(x => x.Email == email.ToLower());
        return user;
    }

    public async Task<User> AddUserAsync(User user)
    {
        await _context.users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
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