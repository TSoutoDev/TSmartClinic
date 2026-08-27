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
    [PermissionModule("Clientes")]
    public class ClientesController : BaseController<Cliente, IClienteService, BaseFiltro, BaseClienteRequestDTO, ClienteUpdateRequestDTO, ClienteResponseDTO>
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService, IMapper mapper) : base(clienteService, mapper)
        {
            _clienteService = clienteService;
        }

        [AuthorizePermission("Acessar")]
        [HttpGet("dropdown-clientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ClienteResponseDTO>>> Obter()
        {
            var lista = await _clienteService.ListarClientes();

            if (lista == null || !lista.Any())
                throw new NotFoundException();

            var obj = Mapper.Map<List<ClienteResponseDTO>>(lista);

            return StatusCode(200, obj);
        }
    }
}