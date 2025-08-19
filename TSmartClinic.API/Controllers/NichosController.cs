using AutoMapper;
using Microsoft.AspNetCore.Mvc;
//using TSmartClinic.API.DTOs.Requests.Base;
//using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Shared.DTOs.Requests.Base;
using TSmartClinic.Shared.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NichosController : BaseController<Nicho, IBaseService<Nicho>, BaseFiltro, BaseNichoRequestDTO, BaseNichoRequestDTO, NichoResponseDTO>
    {
        private readonly INichoService _nichoService;
        public NichosController(INichoService nichoService, IMapper mapper) : base(nichoService, mapper)
        {
            _nichoService = nichoService;
        }

        //[AuthorizePermission("Usuarios_Acessar")]
        [HttpGet("obter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task <ActionResult<List<NichoResponseDTO>>> Obter()
        {
            var lista = await _nichoService.ListarNichos();

            if (lista == null || !lista.Any()) throw new NotFoundException();

            var obj = Mapper.Map<List<NichoResponseDTO>>(lista);

            return StatusCode(200, Mapper.Map<List<NichoResponseDTO>>(obj));
        }
    }
}
