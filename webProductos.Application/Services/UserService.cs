using System.Data;
using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Application.Interfaces.Services;
using webProductos.Domain.Common;
using webProductos.Domain.Entities;
using webProductos.Domain.Repository;

namespace webProductos.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                return false;

            await _repo.DeleteAsync(user);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user with id ${id}");
            throw;
        }
    }

    public async Task<Result> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            return Result.NotFound();

        try
        {
            user.UpdatePartial(dto.email, dto.password);

            var hadChanges = await _repo.SaveChangesAsync();

            if (!hadChanges)
            {
                return Result.Failure("No changes detected.");
            }

            return Result.Ok();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
        catch (DBConcurrencyException)
        {
            return Result.Conflict("Concurrency conflic. Reload and retry.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user with ID ${id}");
            return Result.Failure("An error ocurred while updating the product.");
        }
    }
}