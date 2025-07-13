using AgendaApp.API.DTOs.Requests.Insert;
using AgendaApp.API.DTOs.Requests.Update;
using AgendaApp.API.DTOs.Responses;
using AgendaApp.Core.Domain.Helpers.FilterHelper;
using AgendaApp.Core.Domain.Interfaces.Services;
using AgendaApp.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
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
