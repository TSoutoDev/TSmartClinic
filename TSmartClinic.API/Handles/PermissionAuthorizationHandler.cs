using Microsoft.AspNetCore.Authorization;

namespace TSmartClinic.API.Handles
{
    public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissaoClaims = context.User.FindFirst("permissao")?.Value;
            var permissoes = permissaoClaims?.Split(',') ?? Array.Empty<string>();

            if (permissoes.Contains(requirement.Permissao))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
