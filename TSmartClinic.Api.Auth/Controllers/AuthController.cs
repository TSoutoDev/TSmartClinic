using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Api.Auth.Interfaces.Services;

namespace TSmartClinic.Api.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AuthController(
            IAutenticacaoService autenticacaoService)
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
                var usuario = _autenticacaoService.Login(login);

                if (usuario == null)
                {
                    return Unauthorized("Usuário e/ou senha inválido.");
                }

                return Ok(usuario);
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
            return Ok("Logout realizado");
        }
    }
}