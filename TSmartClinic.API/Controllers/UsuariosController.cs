using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TSmartClinic.API.Handles;
using TSmartClinic.API.Services;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PermissionModule("Usuarios")]
    public class UsuariosController : BaseController<Usuario,IUsuarioService, UsuarioFiltro, UsuarioInsertRequestDTO, UsuarioUpdateRequestDTO, UsuarioResponseDTO>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public UsuariosController(IUsuarioLogadoService usuarioLogadoService, ITokenService tokenService, IUsuarioService usuarioService, IMapper mapper) : base(usuarioService, mapper)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _usuarioLogadoService = usuarioLogadoService;
        }

        [AuthorizePermission("Acessar")]
        [HttpGet("obter-por-email/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult<UsuarioResponseDTO> ObterPorEmail(string email)
        {
            var obj = _usuarioService.ObterPorEmail(email);

            if (obj == null)
                throw new NotFoundException();

            return StatusCode(200, Mapper.Map<UsuarioResponseDTO>(obj));
        }

        [AuthorizePermission("Editar")]
        [HttpPatch("{publicId:guid}/bloquear")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult Bloquear(Guid publicId)
        {
            _usuarioService.Bloquear(publicId);

            return StatusCode(200);
        }

        [HttpPost("primeiro-acesso")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DefinirSenha([FromBody] BaseUsuarioPrimeiroAcessoRequestoDTO req)
        {
            try
            {
                _usuarioService.DefinirSenha(req.Token, req.NovaSenha);

                return Ok(new
                {
                    message = "Senha definida com sucesso."
                });
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-senha")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ResetSenha([FromBody] BaseEsqueciSenhaRequestDTO req)
        {
            try
            {
                _usuarioService.GerarTokenResetSenha(req.Email);

                return Ok(new {message = "Solicitação de redefinição de senha realizada com sucesso."});
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("minha-conta")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioResponseDTO> MinhaConta()
        {
            var usuarioId = _usuarioLogadoService.UsuarioLogadoId;

            if (!usuarioId.HasValue)
                return Unauthorized();

            var usuario = _usuarioService.ObterPorId(usuarioId.Value);

            if (usuario == null)
                return NotFound();

            var response = Mapper.Map<UsuarioResponseDTO>(usuario);

            return Ok(response);
        }
    }
}