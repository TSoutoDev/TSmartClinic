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
        [HttpGet("{publicId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult<PacienteResponseDTO> ObterPorPublicId(Guid publicId)
        {
            var paciente = Service.ObterPorPublicId(publicId);

            if (paciente == null)
                throw new NotFoundException();

            return StatusCode(200, Mapper.Map<PacienteResponseDTO>(paciente));
        }

        [AuthorizePermission("Pacientes_Incluir")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> Inserir(PacienteInsertRequestDTO objRequest)
        {
            ValidarFoto(objRequest.Foto, objRequest.FotoContentType);
            return base.Inserir(objRequest);
        }

        [AuthorizePermission("Pacientes_Editar")]
        [HttpPatch("{publicId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult<PacienteResponseDTO> Atualizar(Guid publicId, PacienteUpdateRequestDTO objRequest)
        {
            ValidarFoto(objRequest.Foto, objRequest.FotoContentType);
            return base.Atualizar(publicId, objRequest);
        }

        [AuthorizePermission("Pacientes_Excluir")]
        [HttpDelete("{publicId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public override ActionResult Excluir(Guid publicId)
        {
            return base.Excluir(publicId);
        }

        #region Métodos auxiliares

        private void ValidarFoto(byte[]? foto, string? contentType)
        {
            if (foto == null || foto.Length == 0)
                return;

            // Foto existente sendo mantida
            if (string.IsNullOrWhiteSpace(contentType))
                return;

            if (foto.Length > 2 * 1024 * 1024)
                throw new Exception("A foto deve ter no máximo 2 MB.");

            var tiposPermitidos = new[]
            {
                "image/jpeg",
                "image/png",
                "image/webp"
            };

            if (!tiposPermitidos.Contains(contentType.ToLower()))
                throw new Exception("Formato de foto inválido. Utilize JPG, PNG ou WEBP.");
        }

        #endregion
    }
}