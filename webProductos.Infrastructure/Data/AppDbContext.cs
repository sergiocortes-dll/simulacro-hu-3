using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;

namespace webProductos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> productos { get; set; }
    
    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
}