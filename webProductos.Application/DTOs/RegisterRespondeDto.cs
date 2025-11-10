using webProductos.Domain.Enums;

namespace webProductos.Application.DTOs;

public class RegisterRespondeDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}