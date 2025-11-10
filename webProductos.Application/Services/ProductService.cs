using System.Data;
using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Application.Interfaces.Services;
using webProductos.Domain.Common;
using webProductos.Domain.Entities;
using webProductos.Domain.Repository;

namespace webProductos.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Product>> GetAllAsync()
    {
        var response = await _repo.GetAllAsync();

        return response;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) 
                return false;

            await _repo.DeleteAsync(product);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product with ID {id}");
            throw;
        }
    }

    public async Task<Result> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) 
            return Result.NotFound();

        try
        {
            product.UpdatePartial(dto.name);
        
            var hadChanges = await _repo.SaveChangesAsync();
        
            if (!hadChanges) 
                return Result.Failure("No changes detected.");

            return Result.Ok();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
        catch (DBConcurrencyException)
        {
            return Result.Conflict("Concurrency conflict. Reload and retry.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product with ID {id}");
            return Result.Failure("An error ocurred while updating the product.");
        }
    }


    public async Task<int> AddProductAsync(ProductAddDto dto)
    {
        var product = new Product(dto.name);
        
        await _repo.AddProductAsync(product);
        await _repo.SaveChangesAsync();

        return product.Id;
    }
}