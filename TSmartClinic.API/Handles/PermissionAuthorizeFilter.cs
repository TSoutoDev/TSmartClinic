using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TSmartClinic.API.Handles
{
    public class PermissionAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly string _requiredPermission;
        public PermissionAuthorizeFilter(IAuthorizationService authorizationService, string requiredPermission)
        {
            _authorizationService = authorizationService;
            _requiredPermission = requiredPermission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var result = await _authorizationService.AuthorizeAsync(
                                     context.HttpContext.User, null, new PermissionRequirement(_requiredPermission));

            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
