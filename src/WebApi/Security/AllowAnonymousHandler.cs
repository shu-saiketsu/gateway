using Microsoft.AspNetCore.Authorization;

namespace Saiketsu.Gateway.WebApi.Security;

public sealed class AllowAnonymousHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}