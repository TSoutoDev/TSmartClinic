using AutoMapper;
using Microsoft.AspNetCore.Mvc;
//using TSmartClinic.API.DTOs.Requests.Insert;
//using TSmartClinic.API.DTOs.Requests.Update;
//using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : BaseController<Paciente, IPacienteService, BaseFiltro, PacienteInsertRequestDTO, PacienteUpdateRequestDTO, PacienteResponseDTO>
    {
        public PacientesController(IPacienteService baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
