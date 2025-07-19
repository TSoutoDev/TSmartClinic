using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Api.Auth.Interfaces.Services;

namespace TSmartClinic.Api.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AuthController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(typeof(LoginResponseDto), 401)]
        public IActionResult Login(LoginRequestDto login)
        {
            try
            {
                var usuario = _autenticacaoService?.Login(login);

                if (usuario == null)
                {
                    return Unauthorized("Usuário e/ou senha inválido.");
                }

                //Response.Cookies.Append("accessToken", usuario.AccessToken, new CookieOptions
                //{
                //    HttpOnly = true,
                //    Secure = true,
                //    SameSite = SameSiteMode.Strict,
                //    Expires = DateTime.UtcNow.AddDays(7)
                //});


                return StatusCode(200, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Route("logout")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Logout()
        {
            //_autenticacaoService?.Logout(usuarioId);

            //return StatusCode(200);
            return Ok("Logout realizado");
        }

    }
}
