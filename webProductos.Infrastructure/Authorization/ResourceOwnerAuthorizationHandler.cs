using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace webProductos.Infrastructure.Authorization;

public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, int>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement requirement,
        int resourceUserId)
    {
        var currentUserId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = context.User.IsInRole("Admin");

        if (currentUserId != null &&
            (int.Parse(currentUserId) == resourceUserId || isAdmin))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class ResourceOwnerRequirement : IAuthorizationRequirement
{
}