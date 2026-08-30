using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TSmartClinic.Presentation.Helpers
{
    public static class PermissionHelper
    {
        public static bool HasPermission(this ClaimsPrincipal user, HttpContext httpContext, string permissao)
        {
            if (user == null || httpContext == null || string.IsNullOrWhiteSpace(permissao))
            {
                return false;
            }

            // Master possui acesso total
            var tipoUsuario = user.FindFirst("Usuario_Tipo")?.Value;

            if (string.Equals(tipoUsuario, "M",StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Permissões agora ficam na Session
            var permissoesSession = httpContext.Session.GetString("Permissoes");

            if (string.IsNullOrWhiteSpace(permissoesSession))
                return false;

            var permissoes = permissoesSession.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return permissoes.Contains(permissao, StringComparer.OrdinalIgnoreCase);
        }
    }
}