using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.DTOs.Requests.Base;
using TSmartClinic.API.DTOs.Responses;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : BaseController<Cliente, IClienteService, BaseFiltro, BaseClienteRequestDTO, BaseClienteRequestDTO, ClienteResponseDTO>
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService, IMapper mapper) : base(clienteService, mapper)
        {
            _clienteService = clienteService;
        }

        //[AuthorizePermission("Usuarios_Acessar")]
        [HttpGet("dropdown-clientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ClienteResponseDTO>>> Obter()
        {
            var lista = await _clienteService.ListarClientes();

            if (lista == null || !lista.Any()) throw new NotFoundException();

            var obj = Mapper.Map<List<ClienteResponseDTO>>(lista);

            return StatusCode(200, Mapper.Map<List<ClienteResponseDTO>>(obj));
        }
    }
}
