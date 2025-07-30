using Microsoft.AspNetCore.Mvc;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Controllers.Pacientes
{
    public class PacientesDadosPartialController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacientesDadosPartialController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public IActionResult PacientesDadosPartial(int id)
        {
            var paciente = _pacienteService.ObterPorId(id);

            if (paciente == null)
                return NotFound();

            var viewModel = new PacienteViewModel
            {
                NomePaciente = paciente.NomePaciente,
                CPF = paciente.CPF,
                DataCadastro = paciente.DataCadastro,
                DataNascimento = paciente.DataNascimento,
                Email = paciente.Email,
                Convenio = paciente.Convenio,
                Telefone = paciente.Telefone,
                Observacoes = paciente.Observacoes
              
            };

            return View(viewModel);
        }
    }
}
