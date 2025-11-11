using webProductos.Domain.Enums;

namespace webProductos.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public User(string email, string password, UserRole role = UserRole.Client)
    {
        Email = email;
        Password = password;
        Role = role;
    }
    
    public User(){}

    public void UpdatePartial(string? email, string? password)
    {
        Email = email!;
        Password = password!;
    }

    public bool IsAdmin() => Role == UserRole.Admin;
    public bool IsOwner(int userId) => Id == userId;
}