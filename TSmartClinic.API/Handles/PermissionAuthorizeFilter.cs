using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.API.Handles
{
    public class PermissionAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly string _operacao;
        public PermissionAuthorizeFilter(IAuthorizationService authorizationService, string operacao)
        {
            _authorizationService = authorizationService;
            _operacao = operacao;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var controllerType = controllerActionDescriptor.ControllerTypeInfo;

            var moduloAttribute = controllerType
               .GetCustomAttributes(typeof(PermissionModuleAttribute), true)
               .OfType<PermissionModuleAttribute>()
               .FirstOrDefault();

            var modulo = moduloAttribute?.Nome;

            // Caso não tenha atributo, usa o nome da Controller
            if (string.IsNullOrWhiteSpace(modulo))
            {
                modulo = controllerActionDescriptor.ControllerName;
            }

            var permissaoCompleta = $"{modulo}_{_operacao}";

            var result = await _authorizationService.AuthorizeAsync(context.HttpContext.User, null, new PermissionRequirement(permissaoCompleta));

            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
