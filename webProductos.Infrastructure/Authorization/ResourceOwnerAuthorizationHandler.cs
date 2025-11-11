using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace webProductos.Infrastructure.Authorization;

public class ResourceOwnerOrAdminAuthorizationHandler : AuthorizationHandler<ResourceOwnerOrAdminRequirement, int>
{
    private readonly ILogger<ResourceOwnerOrAdminAuthorizationHandler> _logger;

    public ResourceOwnerOrAdminAuthorizationHandler(ILogger<ResourceOwnerOrAdminAuthorizationHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerOrAdminRequirement requirement,
        int resourceUserId)
    {
        var currentUserIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        
        if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            return Task.CompletedTask;

        // VERIFICACIÓN FLEXIBLE DE ROLES
        var roleClaims = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        _logger.LogInformation("Role claims: {@Roles}", roleClaims);

        var isAdmin = roleClaims.Any(role => 
            role == "Admin" ||    // Si usas el nombre del enum
            role == "1" ||        // Si usas el valor numérico
            role?.ToLower() == "admin"); // Case-insensitive

        _logger.LogInformation("User {CurrentUserId} vs Resource {ResourceUserId}, IsAdmin: {IsAdmin}", 
            currentUserId, resourceUserId, isAdmin);

        if (currentUserId == resourceUserId || isAdmin)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class ResourceOwnerOrAdminRequirement : IAuthorizationRequirement
{
}