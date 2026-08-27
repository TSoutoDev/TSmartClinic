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
    [PermissionModule("Convenios")]
    public class ConveniosController : BaseController<Convenio, IConvenioService, BaseFiltro, BaseConvenioRequestDTO, BaseConvenioRequestDTO, ConvenioResponseDTO>
    {
        public ConveniosController( IConvenioService convenioService, IMapper mapper)  : base(convenioService, mapper)
        {
        }
    }
}