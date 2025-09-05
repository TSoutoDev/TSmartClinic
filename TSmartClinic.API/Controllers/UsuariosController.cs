//using TSmartClinic.API.DTOs.Requests.Insert;
//using TSmartClinic.API.DTOs.Requests.Update;
//using TSmartClinic.API.DTOs.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
    // [Authorize]
    public class UsuariosController : BaseController<Usuario, IUsuarioService, UsuarioFiltro, UsuarioInsertRequestDTO, UsuarioUpdateRequestDTO, UsuarioResponseDTO>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        public UsuariosController(ITokenService tokenService, IUsuarioService usuarioService, IMapper mapper) : base(usuarioService, mapper)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        //[AuthorizePermission("Usuarios_Acessar")]
        [HttpGet("obter-por-email/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public virtual ActionResult<UsuarioResponseDTO> ObterPorEmail(string email)
        {
            var obj = _usuarioService?.ObterPorEmail(email);

            if (obj == null) throw new NotFoundException();

            return StatusCode(200, Mapper.Map<UsuarioResponseDTO>(obj));
        }

        //  [AuthorizePermission("Usuarios_Editar")]
        [HttpPatch("{id}/bloquear")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual ActionResult<UsuarioResponseDTO> Bloquear(int id)
        {
            _usuarioService?.Bloquear(id);

            return StatusCode(200);
        }

        // POST /api/usuarios/primeiro-acesso
        [HttpPost("primeiro-acesso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DefinirSenha([FromBody] BaseUsuarioPrimeiroAcessoRequestoDTO req)
        {
            try
            {
                _usuarioService.DefinirSenha(req.Token, req.NovaSenha);
                return Ok(new { message = "Senha definida com sucesso." });
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [AuthorizePermission("Usuarios_Acessar")]
        public override ActionResult<ResponseDTO<UsuarioResponseDTO>> Listar(UsuarioFiltro filtro)
        {
            return base.Listar(filtro);
        }

        // [AuthorizePermission("Usuarios_Acessar")]
        public override ActionResult<UsuarioResponseDTO> ObterPorId(int id)
        {
            return base.ObterPorId(id);
        }

        //  [AuthorizePermission("Usuarios_Incluir")]
        public override ActionResult<UsuarioResponseDTO> Inserir(UsuarioInsertRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        //  [AuthorizePermission("Usuarios_Editar")]
        public override ActionResult<UsuarioResponseDTO> Atualizar(int id, UsuarioUpdateRequestDTO objRequest)
        {
            return base.Atualizar(id, objRequest);
        }

        //  [AuthorizePermission("Usuarios_Excluir")]
        public override ActionResult Excluir(int id)
        {
            return base.Excluir(id);
        }


    }
}
