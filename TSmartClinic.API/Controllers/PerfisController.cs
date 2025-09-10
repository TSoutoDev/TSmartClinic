using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : BaseController<Perfil, IPerfilService, BaseFiltro,BasePerfilRequestDTO,PerfilUpdateRequestDTO, PerfilResponseDTO>
    {
        private readonly IPerfilService _perfilService;
        public PerfisController(IPerfilService perfilService, IPerfilService baseService, IMapper mapper) : base(baseService, mapper)
        {
            _perfilService = perfilService;
        }

        [AuthorizePermission("Perfis_Acessar")]
        [Authorize(Roles = "Master")]
        [HttpGet("dropdown-perfil/{idCliente}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<PerfilResponseDTO>>> Obter(int idCliente)
        {
            var lista = await _perfilService.ListarPerfilPorCliente(idCliente);

            if (lista == null || !lista.Any()) throw new NotFoundException();

            var obj = Mapper.Map<List<PerfilResponseDTO>>(lista);

            return StatusCode(200, Mapper.Map<List<PerfilResponseDTO>>(obj));
        }

        [AuthorizePermission("Perfis_Acessar")]
        public override ActionResult<ResponseDTO<PerfilResponseDTO>> Listar(BaseFiltro filtro)
        {
            return base.Listar(filtro);
        }

        [AuthorizePermission("Perfis_Acessar")]
        public override ActionResult<PerfilResponseDTO> ObterPorId(int id)
        {
            return base.ObterPorId(id);
        }

        [AuthorizePermission("Perfis_Incluir")]
        public override ActionResult<PerfilResponseDTO> Inserir(BasePerfilRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Perfis_Editar")]
        public override ActionResult<PerfilResponseDTO> Atualizar(int id, PerfilUpdateRequestDTO objRequest)
        {
            return base.Atualizar(id, objRequest);
        }

        [AuthorizePermission("Perfis_Excluir")]
        public override ActionResult Excluir(int id)
        {
            return base.Excluir(id);
        }
    }
}
