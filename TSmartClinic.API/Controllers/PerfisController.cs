using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.DTOs.Requests.Base;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : BaseController<Perfil, IPerfilService, BaseFiltro, BasePerfilRequestDTO, BasePerfilRequestDTO, PerfilResponseDTO>
    {
        public PerfisController(IPerfilService baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
