using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TSmartClinic.Presentation.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "asp-permissao")]
    public class PermissionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("asp-permissao")]
        public string Permissoes { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                output.SuppressOutput();
                return;
            }

            // Master possui acesso total
            var masterClaim = user.FindFirst("UsuarioMaster")?.Value;

            if (bool.TryParse(masterClaim, out var usuarioMaster) && usuarioMaster)
                return;

            var requiredPermissions = Permissoes
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();

            var userPermissions = user.Claims
                .Where(c => c.Type == "permissao")
                .SelectMany(c => c.Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var temPermissao = requiredPermissions.Any(p => userPermissions.Contains(p));

            if (!temPermissao)
                output.SuppressOutput();
        }
    }
}