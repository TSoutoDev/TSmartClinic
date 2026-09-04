using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Services;

//using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Responses.PermissoesAcessoRersponse;

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
        private readonly IPerfilService _perfilService;
        private readonly IPermissaoCacheService _permissaoCacheService;

        public PermissoesAcessoController(IPermissaoCacheService permissaoCacheService, IMapper mapper, IFuncionalidadeService funcionalidadeService, IModuloService moduloService, IOperacaoService operacaoService, IPerfilService perfilService)
        {
            _moduloService = moduloService;
            _operacaoService = operacaoService;
            _funcionalidadeService = funcionalidadeService;
            _mapper = mapper;
            _perfilService = perfilService;
            _permissaoCacheService = permissaoCacheService;


        }

        [HttpGet("permissoes-acesso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PermissoesAcessoResponseDTO.ModuloResponseDTO>>> ObterModuloPermissoes(CancellationToken ct)
        {
            var lista = await _moduloService.ListarPermissoesAsync(ct);

            if (lista is null || lista.Count == 0)
                return NoContent();

            var dto = _mapper.Map<List<PermissoesAcessoResponseDTO.ModuloResponseDTO>>(lista);
            return Ok(dto);
        }


        [HttpGet("permissoes-acesso/{publicId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<int>>> ObterOperacoesDoPerfil(Guid publicId, CancellationToken ct)
        {
            var perfil = _perfilService.ObterPorPublicId(publicId);

            if (perfil == null)
                return NotFound();

            var ids = await _moduloService.ListarIdsPorPerfilAsync(perfil.Id, ct);

            if (ids == null || ids.Count == 0)
                return NoContent();

            return Ok(ids);
        }


        [HttpPut("permissoes-acesso/{publicId:guid}/operacoes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SalvarOperacoesDoPerfil( Guid publicId,  [FromBody] List<int> operacaoIds, CancellationToken ct)
        {
            var perfil = _perfilService.ObterPorPublicId(publicId);

            if (perfil == null)
                return NotFound();

            await _moduloService.AtualizarOperacoesDoPerfilAsync(perfil.Id, operacaoIds ?? new List<int>(), ct);

            return NoContent();
        }

        [Authorize]
        [HttpPost("limpar-cache")]
        public IActionResult LimparCache()
        {
            var usuarioIdClaim = User.FindFirst("Usuario_Id")?.Value;
            var unidadeIdClaim = User.FindFirst("Unidade_Id")?.Value;

            if (!int.TryParse(usuarioIdClaim, out var usuarioId))
                return Unauthorized();

            if (!int.TryParse(unidadeIdClaim, out var unidadeId))
                return Unauthorized();

            _permissaoCacheService.RemoverPermissoes(usuarioId, unidadeId);

            return Ok();
        }

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
