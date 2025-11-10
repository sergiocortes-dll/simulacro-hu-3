using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using webProductos.Application.Common;
using webProductos.Application.DTOs;
using webProductos.Application.Interfaces.Services;
using webProductos.Domain.Entities;
using webProductos.Domain.Enums;
using webProductos.Domain.Repository;

namespace webProductos.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);

        if (user == null || !new Password(loginDto.Password, user.Password).VerifyHash())
            throw new UnauthorizedAccessException("Invalid credentials.");

        var token = GenerateJwtToken(user);

        return new LoginResponseDto
        {
            Token = token,
            Expires = DateTime.UtcNow.AddMinutes(GetJwtExpireMinutes()),
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<RegisterRespondeDto> RegisterAsync(RegisterDto registerdto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerdto.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("El email ya está registrado.");
        }

        var hashedPassword = new Password(registerdto.Password).Hash();

        var user = new User
        {
            Email = registerdto.Email,
            Password = hashedPassword,
            Role = UserRole.Client
        };

        var createdUser = await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();

        return new RegisterRespondeDto
        {
            Id = createdUser.Id,
            Email = createdUser.Email,
            Role = createdUser.Role,
            CreatedAt = DateTime.UtcNow
        };
    }


    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtKey()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: GetJwtIssuer(),
            audience: GetJwtAudience(),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(GetJwtExpireMinutes()),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private string GetJwtKey() => _configuration["Jwt:Key"];
    private string GetJwtIssuer() => _configuration["Jwt:Issuer"];
    private string GetJwtAudience() => _configuration["Jwt:Audience"];
    private int GetJwtExpireMinutes() => int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "60");
}