using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : BaseController<Paciente,IPacienteService, BaseFiltro, PacienteInsertRequestDTO, PacienteUpdateRequestDTO, PacienteResponseDTO>
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService, IMapper mapper) : base(pacienteService, mapper)
        {
            _pacienteService = pacienteService;
        }

        private int ObterClienteId()
        {
            var claim = User.FindFirst("Cliente_Id");

            if (claim == null || !int.TryParse(claim.Value, out var clienteId) || clienteId <= 0)
                throw new UnauthorizedAccessException("Clínica do usuário não identificada.");

            return clienteId;
        }

        [AuthorizePermission("Pacientes_Acessar")]
        [HttpPost("listar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ResponseDTO<PacienteResponseDTO>> Listar(BaseFiltro filtro)
        {
            var clienteId = ObterClienteId();

            var lista = _pacienteService.ListarPorCliente(filtro, clienteId);

            if (lista == null || !lista.Any())
                throw new NotFoundException();

            var itens = Mapper.Map<List<PacienteResponseDTO>>(lista);

            return StatusCode(200, new ResponseDTO<PacienteResponseDTO>
            {
                QuantidadeRegistros = itens.Count,
                PaginaAtual = filtro.PaginaAtual,
                Itens = itens
            });
        }

        [AuthorizePermission("Pacientes_Acessar")]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> ObterPorId(int id)
        {
            var clienteId = ObterClienteId();

            var paciente = _pacienteService.ObterPorIdCliente(id, clienteId);

            if (paciente == null)
                throw new NotFoundException();

            return StatusCode(200, Mapper.Map<PacienteResponseDTO>(paciente)
            );
        }

        [AuthorizePermission("Pacientes_Incluir")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> Inserir(PacienteInsertRequestDTO objRequest)
        {
            var clienteId = ObterClienteId();

            var paciente = Mapper.Map<Paciente>(objRequest);

            paciente.ClienteId = clienteId;

            _pacienteService.Inserir(paciente);

            return StatusCode(201, Mapper.Map<PacienteResponseDTO>(paciente)
            );
        }

        [AuthorizePermission("Pacientes_Editar")]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> Atualizar(
            int id,
            PacienteUpdateRequestDTO objRequest)
        {
            var clienteId = ObterClienteId();

            var pacienteExistente = _pacienteService.ObterPorIdCliente(id, clienteId);

            if (pacienteExistente == null)
                throw new NotFoundException();

            var pacienteAlteracao = Mapper.Map<Paciente>(objRequest);

            pacienteAlteracao.ClienteId = pacienteExistente.ClienteId;

            var pacienteAtualizado = _pacienteService.Atualizar(id, pacienteAlteracao);

            return StatusCode(200, Mapper.Map<PacienteResponseDTO>(pacienteAtualizado));
        }

        [AuthorizePermission("Pacientes_Excluir")]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult Excluir(int id)
        {
            var clienteId = ObterClienteId();

            var paciente = _pacienteService.ObterPorIdCliente(id, clienteId);

            if (paciente == null)
                throw new NotFoundException();

            _pacienteService.Excluir(id);

            return StatusCode(200);
        }
    }
}