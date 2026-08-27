using Microsoft.AspNetCore.Authorization;

namespace TSmartClinic.API.Handles
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var masterClaim = context.User.FindFirst("UsuarioMaster")?.Value;

            if (bool.TryParse(masterClaim, out var usuarioMaster) && usuarioMaster)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var permissoes = context.User.Claims
                .Where(c => c.Type == "permissao")
                .SelectMany(c => c.Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (permissoes.Contains(requirement.Permissao))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}