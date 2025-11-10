using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Domain.Entities;

namespace webProductos.Application.Interfaces.Services;

public interface IUserService
{
    
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Result> UpdateAsync(int id, UserUpdateDto dto);
}