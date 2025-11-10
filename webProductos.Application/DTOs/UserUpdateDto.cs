using webProductos.Domain.Enums;

namespace webProductos.Application.DTOs;

public class UserUpdateDto
{
    public string? email { get; init; }
    public string? password { get; init; }
    public UserRole? role { get; init; }
}