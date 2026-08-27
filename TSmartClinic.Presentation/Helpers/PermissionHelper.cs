using System.Security.Claims;

namespace TSmartClinic.Presentation.Helpers
{
    public static class PermissionHelper
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permissao)
        {
            if (user == null || string.IsNullOrWhiteSpace(permissao))
                return false;

            // Usuário Master possui acesso total
            var masterClaim = user.FindFirst("UsuarioMaster")?.Value;

            if (bool.TryParse(masterClaim, out var usuarioMaster) && usuarioMaster)
                return true;

            // Usuários comuns precisam possuir a permissão
            var permissoesClaim = user.FindFirst("permissao")?.Value;

            if (string.IsNullOrWhiteSpace(permissoesClaim))
                return false;

            var permissoes = permissoesClaim.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return permissoes.Contains(permissao, StringComparer.OrdinalIgnoreCase);
        }
    }
}