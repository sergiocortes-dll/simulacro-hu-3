using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;

namespace webProductos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> products { get; set; }
    public DbSet<User> users { get; set; }
    
    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
}