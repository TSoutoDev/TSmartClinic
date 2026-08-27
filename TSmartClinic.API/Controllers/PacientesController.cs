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
    [PermissionModule("Pacientes")]
    public class PacientesController : BaseController<Paciente, IPacienteService, BaseFiltro, PacienteInsertRequestDTO, PacienteUpdateRequestDTO, PacienteResponseDTO>
    {
        public PacientesController(IPacienteService pacienteService, IMapper mapper) : base(pacienteService, mapper)
        {
        }

        [HttpPost]
        public override ActionResult<PacienteResponseDTO> Inserir( PacienteInsertRequestDTO objRequest)
        {
            ValidarFoto(objRequest.Foto, objRequest.FotoContentType);

            return base.Inserir(objRequest);
        }

        [HttpPatch("{publicId:guid}")]
        public override ActionResult<PacienteResponseDTO> Atualizar(Guid publicId, PacienteUpdateRequestDTO objRequest)
        {
            ValidarFoto(objRequest.Foto, objRequest.FotoContentType);

            return base.Atualizar(publicId, objRequest);
        }


        #region Métodos auxiliares
        private void ValidarFoto(byte[]? foto, string? contentType)
        {
            if (foto == null || foto.Length == 0)
                return;

            // Foto já existente sendo mantida
            if (string.IsNullOrWhiteSpace(contentType))
                return;

            if (foto.Length > 2 * 1024 * 1024)
                throw new Exception( "A foto deve ter no máximo 2 MB.");

            var tiposPermitidos = new[]
            {
                "image/jpeg",
                "image/png",
                "image/webp"
            };

            if (!tiposPermitidos.Contains(contentType, StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception("Formato de foto inválido. Utilize JPG, PNG ou WEBP.");
            }
        }

        #endregion
    }
}