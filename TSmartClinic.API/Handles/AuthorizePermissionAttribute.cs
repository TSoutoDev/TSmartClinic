using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.API.Handles
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizePermissionAttribute : TypeFilterAttribute
    {
        public AuthorizePermissionAttribute(string permissao) : base(typeof(PermissionAuthorizeFilter))
        {
            Arguments = new object[] { permissao };
        }
    }
}
