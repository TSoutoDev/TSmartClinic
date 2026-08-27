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
    [PermissionModule("Perfis")]
    public class PerfisController : BaseController<Perfil, IPerfilService, BaseFiltro, BasePerfilRequestDTO, PerfilUpdateRequestDTO,
        PerfilResponseDTO>
    {
        private readonly IPerfilService _perfilService;

        public PerfisController(IPerfilService perfilService, IMapper mapper) : base(perfilService, mapper)
        {
            _perfilService = perfilService;
        }

        [AuthorizePermission("Acessar")]
        [HttpGet("dropdown-perfil/{idCliente}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<PerfilResponseDTO>>> Obter(int idCliente)
        {
            var lista = await _perfilService.ListarPerfilPorCliente(idCliente);

            if (lista == null || !lista.Any())
                throw new NotFoundException();

            var obj = Mapper.Map<List<PerfilResponseDTO>>(lista);

            return StatusCode(200, obj);
        }
    }
}