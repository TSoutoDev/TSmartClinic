using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosController : BaseController<Convenio, IConvenioService, BaseFiltro, BaseConvenioRequestDTO, BaseConvenioRequestDTO, ConvenioResponseDTO>
    {
        public ConveniosController(IConvenioService convenioService, IMapper mapper) : base(convenioService, mapper)
        {
        }

        [AuthorizePermission("Convenios_Acessar")]
        [HttpPost("listar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ResponseDTO<ConvenioResponseDTO>> Listar(BaseFiltro filtro)
        {
            return base.Listar(filtro);
        }

        [AuthorizePermission("Convenios_Acessar")]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ConvenioResponseDTO> ObterPorId(int id)
        {
            return base.ObterPorId(id);
        }

        [AuthorizePermission("Convenios_Incluir")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ConvenioResponseDTO> Inserir(BaseConvenioRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Convenios_Editar")]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ConvenioResponseDTO> Atualizar(Guid publicId, BaseConvenioRequestDTO objRequest)
        {
            return base.Atualizar(publicId, objRequest);
        }

        [AuthorizePermission("Convenios_Excluir")]
        [HttpDelete("{id}")]
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