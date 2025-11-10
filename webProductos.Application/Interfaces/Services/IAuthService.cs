using webProductos.Application.DTOs;
using webProductos.Domain.Entities;

namespace webProductos.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<RegisterRespondeDto> RegisterAsync(RegisterDto registerdto);
    string GenerateJwtToken(User user);
}