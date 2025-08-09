using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class PermissoesAcessoController : ControllerBase
    {
        private readonly IModuloService _moduloService;
        private readonly IMapper _mapper;
        public PermissoesAcessoController(IMapper mapper, IModuloService moduloService)
        {
            _moduloService = moduloService;
            _mapper = mapper;
        }

        //[AuthorizePermission("Usuarios_Acessar")]
        [HttpGet("modulos")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ModuloResponseDTO>>> ObterTodos()
        {
            var lista = await _moduloService.ListarModulos();

            if (lista == null || !lista.Any()) throw new NotFoundException();

            var obj = _mapper.Map<List<ModuloResponseDTO>>(lista);

            return StatusCode(200, _mapper.Map<List<ModuloResponseDTO>>(obj));
        }
    }
}
