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
        private readonly IFuncionalidadeService _funcionalidadeService;
        private readonly IMapper _mapper;
        public PermissoesAcessoController(IMapper mapper, IFuncionalidadeService funcionalidadeService, IModuloService moduloService, IOperacaoService operacaoService)
        {
            _moduloService = moduloService;
            _operacaoService = operacaoService;
            _funcionalidadeService = funcionalidadeService;
            _mapper = mapper;
        }

        [HttpGet("permissoes-acesso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ModuloResponseDTO>>> ObterModuloPermissoes(CancellationToken ct)
        {
            var lista = await _moduloService.ListarPermissoesAsync(ct);

            if (lista is null || lista.Count == 0)
                return NoContent();

            var dto = _mapper.Map<List<ModuloResponseDTO>>(lista);
            return Ok(dto);
        }

        //[HttpGet("perfis/{perfilId}/operacoes")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<ActionResult<List<int>>> ObterOperacoesDoPerfil(int perfilId, CancellationToken ct)
        //{
        //    // Supondo que exista um método no service que retorne os IDs das operações do perfil
        //    var ids = await _operacaoService.ListarIdsPorPerfilAsync(perfilId, ct);

        //    if (ids == null || ids.Count == 0)
        //        return NoContent();

        //    return Ok(ids);
        //}

        //[HttpPut("perfis/{perfilId}/operacoes")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> SalvarOperacoesDoPerfil(int perfilId, [FromBody] List<int> operacaoIds, CancellationToken ct)
        //{
        //    // Supondo que exista um método para persistir as operações do perfil
        //    await _operacaoService.SalvarIdsDoPerfilAsync(perfilId, operacaoIds ?? new List<int>(), ct);
        //    return NoContent();
        //}




        //[AuthorizePermission("Usuarios_Acessar")]
        //[HttpGet("modulos")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(500)]
        //public async Task<ActionResult<List<ModuloResponseDTO>>> ObterModulos()
        //{
        //    var lista = await _moduloService.ListarModulos();

        //    if (lista == null || !lista.Any()) throw new NotFoundException();

        //    var obj = _mapper.Map<List<ModuloResponseDTO>>(lista);

        //    return StatusCode(200, _mapper.Map<List<ModuloResponseDTO>>(obj));
        //}


        ////[AuthorizePermission("Usuarios_Acessar")]
        //[HttpGet("operacoes")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(500)]
        //public async Task<ActionResult<List<OperacaoResponseDTO>>> ObterOperacoes()
        //{
        //    var lista = await _operacaoService.ListarOperacoes();

        //    if (lista == null || !lista.Any()) throw new NotFoundException();

        //    var obj = _mapper.Map<List<OperacaoResponseDTO>>(lista);

        //    return StatusCode(200, _mapper.Map<List<OperacaoResponseDTO>>(obj));
        //}

        ////[AuthorizePermission("Usuarios_Acessar")]
        //[HttpGet("funcionalidades")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(500)]
        //public async Task<ActionResult<List<FuncionalidadeResponseDTO>>> ObterFuncionalidades()
        //{
        //    var lista = await _funcionalidadeService.ListarFuncionalidades();

        //    if (lista == null || !lista.Any()) throw new NotFoundException();

        //    var obj = _mapper.Map<List<FuncionalidadeResponseDTO>>(lista);

        //    return StatusCode(200, _mapper.Map<List<FuncionalidadeResponseDTO>>(obj));
        //}
    }
}
