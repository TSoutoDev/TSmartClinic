using Microsoft.AspNetCore.Razor.TagHelpers;
using TSmartClinic.Presentation.Helpers;

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
        public string Permissoes { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                output.SuppressOutput();
                return;
            }

            var user = httpContext.User;

            if (user == null)
            {
                output.SuppressOutput();
                return;
            }

            var permissoesNecessarias = Permissoes
                .Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries |
                    StringSplitOptions.TrimEntries)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (!permissoesNecessarias.Any())
            {
                output.SuppressOutput();
                return;
            }

            var temPermissao = permissoesNecessarias.Any(permissao => user.HasPermission(httpContext, permissao));

            if (!temPermissao)
            {
                output.SuppressOutput();
            }
        }
    }
}