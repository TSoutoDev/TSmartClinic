using Microsoft.AspNetCore.Authorization;

namespace TSmartClinic.API.Handles
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permissao { get; }

        public PermissionRequirement(string permissao)
        {
            Permissao = permissao;
        }
    }
}
