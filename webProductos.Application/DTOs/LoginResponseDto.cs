using webProductos.Domain.Enums;

namespace webProductos.Application.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }

}