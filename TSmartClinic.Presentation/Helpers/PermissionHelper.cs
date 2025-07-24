using System.Security.Claims;

namespace TSmartClinic.Presentation.Helpers
{
    public static class PermissionHelper
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permissao)
        {
            if (user == null || string.IsNullOrWhiteSpace(permissao))
                return false;

            var permissoesClaim = user.FindFirst("permissao")?.Value;
            if (string.IsNullOrWhiteSpace(permissoesClaim))
                return false;

            var permissoes = permissoesClaim.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return permissoes.Contains(permissao);
        }
    }
}
