using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSmartClinic.API.Handles;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;
using TSmartClinic.Shared.DTOs.Responses;

namespace TSmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : BaseController<Paciente, IPacienteService, BaseFiltro, PacienteInsertRequestDTO, PacienteUpdateRequestDTO, PacienteResponseDTO>
    {
        public PacientesController(IPacienteService pacienteService, IMapper mapper) : base(pacienteService, mapper)
        {
        }

        [AuthorizePermission("Pacientes_Acessar")]
        [HttpPost("listar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<ResponseDTO<PacienteResponseDTO>> Listar(BaseFiltro filtro)
        {
            return base.Listar(filtro);
        }

        [AuthorizePermission("Pacientes_Acessar")]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> ObterPorId(int id)
        {
            return base.ObterPorId(id);
        }

        [AuthorizePermission("Pacientes_Incluir")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> Inserir(PacienteInsertRequestDTO objRequest)
        {
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Pacientes_Editar")]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> Atualizar(int id, PacienteUpdateRequestDTO objRequest)
        {
            return base.Atualizar(id, objRequest);
        }

        [AuthorizePermission("Pacientes_Excluir")]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult Excluir(int id)
        {
            return base.Excluir(id);
        }
    }
}