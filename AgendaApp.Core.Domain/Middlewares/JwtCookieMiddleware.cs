using Microsoft.AspNetCore.Http;

namespace AgendaApp.Core.Domain.Middlewares
{
    public class JwtCookieMiddleware 
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext contex)
        {
            if (!contex.Request.Headers.ContainsKey("Authorization"))
            {
                if (contex.Request.Cookies.ContainsKey("accessToken"))
                {
                    var token = contex.Request.Cookies["accessToken"];
                    contex.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }
            await _next(contex);
        }
    }
}
