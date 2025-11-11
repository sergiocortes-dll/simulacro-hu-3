using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Domain.Entities;

namespace webProductos.Application.Interfaces.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Result> UpdateAsync(int id, ProductUpdateDto dto);
    Task<int> AddProductAsync(ProductAddDto productAddDto);
}