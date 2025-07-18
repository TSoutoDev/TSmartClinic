using TSmartClinic.API.DTOs.Requests.Insert;
using TSmartClinic.API.DTOs.Requests.Update;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : BaseController<Categoria, ICategoriaService, BaseFiltro, CategoriaInsertRequestDTO, CategoriaUpdateRequestDTO, CategoriaResponseDTO>
    {
        public CategoriasController(ICategoriaService baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
