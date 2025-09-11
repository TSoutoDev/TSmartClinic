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
    public class ClientesController : BaseController<Cliente, IClienteService, BaseFiltro, BaseClienteRequestDTO, ClienteUpdateRequestDTO, ClienteResponseDTO>
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService, IMapper mapper) : base(clienteService, mapper)
        {
            _clienteService = clienteService;
        }

        [AuthorizePermission("Clientes_Acessar")]
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

        [AuthorizePermission("Clientes_Acessar")]
        public override ActionResult<ResponseDTO<ClienteResponseDTO>> Listar(BaseFiltro filtro)
        {
            return base.Listar(filtro);
        }

        [AuthorizePermission("Clientes_Acessar")]
        public override ActionResult<ClienteResponseDTO> ObterPorId(int id)
        {
            return base.ObterPorId(id);
        }

        [AuthorizePermission("Clientes_Incluir")]
        public override ActionResult<ClienteResponseDTO> Inserir(BaseClienteRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Clientes_Editar")]
        public override ActionResult<ClienteResponseDTO> Atualizar(int id, ClienteUpdateRequestDTO objRequest)
        {
            return base.Atualizar(id, objRequest);
        }

        [AuthorizePermission("Clientes_Excluir")]
        public override ActionResult Excluir(int id)
        {
            return base.Excluir(id);
        }
    }
}
