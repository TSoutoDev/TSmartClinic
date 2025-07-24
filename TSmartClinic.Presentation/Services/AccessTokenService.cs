using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string chave_token = "tsmartclinic-access-token";

        public AccessTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Excluir()
        {
            var context = GetHttpContext();

            if (context == null) return;

            context?.Response.Cookies.Delete(chave_token);
        }

        public string Obter()
        {
            var context = GetHttpContext();
            if (context == null) return null;

            string accessToken = context.Request.Cookies[chave_token];

            if (string.IsNullOrEmpty(accessToken)) return null;

            return accessToken;
        }

        public void Salvar(string accessToken)
        {
            var context = GetHttpContext();

            if (context == null) return;

            var cookieOptions = CreateCookieOptions();
            context.Response.Cookies.Append(chave_token, accessToken, cookieOptions); ;
        }

        private CookieOptions CreateCookieOptions()
        {
            return new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddHours(36),
                IsEssential = true,
                Secure = true
            };
        }

        private HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }
    }
}
