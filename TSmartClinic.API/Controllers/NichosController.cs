using AutoMapper;
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
    public class NichosController : BaseController< Nicho, IBaseService<Nicho>, BaseFiltro, BaseNichoRequestDTO, NichoUpdateRequestDTO, NichoResponseDTO>
    {
        private readonly INichoService _nichoService;

        public NichosController(INichoService nichoService, IMapper mapper) : base(nichoService, mapper)
        {
            _nichoService = nichoService;
        }

        [HttpGet("obter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<NichoResponseDTO>>> Obter()
        {
            var lista = await _nichoService.ListarNichos();

            if (lista == null || !lista.Any())
                throw new NotFoundException();

            var obj = Mapper.Map<List<NichoResponseDTO>>(lista);

            return StatusCode(200, obj);
        }

        [AuthorizePermission("Nichos_Acessar")]
        [HttpPost("listar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ResponseDTO<NichoResponseDTO>> Listar(BaseFiltro filtro)
        {
            return base.Listar(filtro);
        }

        [AuthorizePermission("Nichos_Acessar")]
        [HttpGet("{publicId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult<NichoResponseDTO> ObterPorPublicId(Guid publicId)
        {
            var nicho = Service.ObterPorPublicId(publicId);

            if (nicho == null)
                throw new NotFoundException();

            return StatusCode(200, Mapper.Map<NichoResponseDTO>(nicho));
        }

        [AuthorizePermission("Nichos_Incluir")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<NichoResponseDTO> Inserir(BaseNichoRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Nichos_Editar")]
        [HttpPatch("{publicId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<NichoResponseDTO> Atualizar(Guid publicId, NichoUpdateRequestDTO objRequest)
        {
            return base.Atualizar(publicId, objRequest);
        }

        [AuthorizePermission("Nichos_Excluir")]
        [HttpDelete("{publicId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult Excluir(Guid publicId)
        {
            return base.Excluir(publicId);
        }
    }
}