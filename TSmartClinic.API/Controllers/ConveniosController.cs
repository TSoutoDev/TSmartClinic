using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniosController : BaseController<Convenio, IConvenioService, BaseFiltro, BaseConvenioRequestDTO, BaseConvenioRequestDTO, ConvenioResponseDTO>
    {
        private readonly IConvenioService _convenioService;

        public ConveniosController(IConvenioService convenioService, IMapper mapper) : base(convenioService, mapper)
        {
            _convenioService = convenioService;
        }

        [AuthorizePermission("Convenios_Acessar")]
        public override ActionResult<ResponseDTO<ConvenioResponseDTO>> Listar(BaseFiltro filtro)
        {
            return base.Listar(filtro);
        }

        [AuthorizePermission("Convenios_Acessar")]
        public override ActionResult<ConvenioResponseDTO> ObterPorId(int id)
        {
            return base.ObterPorId(id);
        }

        [AuthorizePermission("Convenios_Incluir")]
        public override ActionResult<ConvenioResponseDTO> Inserir(BaseConvenioRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Convenios_Editar")]
        public override ActionResult<ConvenioResponseDTO> Atualizar(int id, BaseConvenioRequestDTO objRequest)
        {
            return base.Atualizar(id, objRequest);
        }

        [AuthorizePermission("Convenios_Excluir")]
        public override ActionResult Excluir(int id)
        {
            return base.Excluir(id);
        }
    }
}