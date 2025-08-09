using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.API.Services;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class PermissoesAcessoController : ControllerBase
    {
        private readonly IModuloService _moduloService;
        private readonly IOperacaoService _operacaoService;
        private readonly IMapper _mapper;
        public PermissoesAcessoController(IMapper mapper, IModuloService moduloService, IOperacaoService operacaoService)
        {
            _moduloService = moduloService;
            _operacaoService = operacaoService;
            _mapper = mapper;
        }

        //[AuthorizePermission("Usuarios_Acessar")]
        [HttpGet("modulos")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ModuloResponseDTO>>> ObterModulos()
        {
            var lista = await _moduloService.ListarModulos();

            if (lista == null || !lista.Any()) throw new NotFoundException();

            var obj = _mapper.Map<List<ModuloResponseDTO>>(lista);

            return StatusCode(200, _mapper.Map<List<ModuloResponseDTO>>(obj));
        }

        //[AuthorizePermission("Usuarios_Acessar")]
        [HttpGet("operacoes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<OperacaoResponseDTO>>> ObterOperacoes()
        {
            var lista = await _operacaoService.ListarOperacoes();

            if (lista == null || !lista.Any()) throw new NotFoundException();

            var obj = _mapper.Map<List<OperacaoResponseDTO>>(lista);

            return StatusCode(200, _mapper.Map<List<OperacaoResponseDTO>>(obj));
        }
    }
}
