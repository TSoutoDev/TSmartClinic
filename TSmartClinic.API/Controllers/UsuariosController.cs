using TSmartClinic.API.DTOs.Requests.Insert;
using TSmartClinic.API.DTOs.Requests.Update;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController<Usuario, IUsuarioService, UsuarioFiltro, UsuarioInsertRequestDTO, UsuarioUpdateRequestDTO, UsuarioResponseDTO>
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService, IMapper mapper) : base(usuarioService, mapper)
        {
            _usuarioService = usuarioService;
        }


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


        [HttpPatch("{id}/bloquear")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual ActionResult<UsuarioResponseDTO> Bloquear(int id)
        {
            _usuarioService?.Bloquear(id);

            return StatusCode(200);
        }
    }
}
