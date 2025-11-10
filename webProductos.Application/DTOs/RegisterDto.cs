using System.ComponentModel.DataAnnotations;
using webProductos.Domain.Enums;

namespace webProductos.Application.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    
    public UserRole Role { get; set; }
}