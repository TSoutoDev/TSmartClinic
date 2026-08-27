using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Controllers.Pacientes
{
    public class PacientesDadosPartialController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacientesDadosPartialController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public async Task<IActionResult> PacientesDadosPartial(Guid publicId)
        {
            var paciente = await _pacienteService.ObterPorPublicId(publicId);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }
    }
}