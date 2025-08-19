//using TSmartClinic.API.DTOs.Requests.Insert;
//using TSmartClinic.API.DTOs.Requests.Update;
//using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : BaseController<Tarefa, IBaseService<Tarefa>, BaseFiltro, TarefaInsertRequestDTO, TarefaUpdateRequestDTO, TarefaResponseDTO>
    {
        public TarefasController(IBaseService<Tarefa> baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
