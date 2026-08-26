using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class PacientesController : BaseController<IPacienteService, BaseFilterViewModel, PacienteViewModel>
    {
        private readonly IPacienteService _pacienteService;
        private readonly IConvenioService _convenioService;
        public PacientesController(IPacienteService pacienteService, IConvenioService convenioService) : base(pacienteService)
        {
            _pacienteService = pacienteService;
            _convenioService = convenioService;
        }

        [HttpGet]
        public override async Task<IActionResult> Consulta()
        {
            return await base.Consulta();
        }

        [HttpPost]
        public override async Task<IActionResult> Cadastro(PacienteViewModel model)
        {
            var result = await base.Cadastro(model);

            await CriarViewBags();

            return result;
        }

        [HttpGet]
        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();

            var result = await base.Cadastro(id) as ViewResult;

            if (result?.Model is PacienteViewModel model)
            {
                // CADASTRO NOVO
                // Obtem cliente do claim
                var clienteIdClaim = User.FindFirst("Cliente_Id")?.Value;

                if (int.TryParse(clienteIdClaim, out var clienteId))
                {
                    model.ClienteId = clienteId;
                }

                if (!id.HasValue)
                {
                    model.Ativo = true;
                    model.DataCadastro = DateTime.Today;
                }
            }
            return result;
        }

        [HttpGet]
        public override async Task<IActionResult> BuscaAvancada(BaseFilterViewModel filtro)
        {
            return await base.BuscaAvancada(filtro);
        }

        [HttpGet]
        public async Task<IActionResult> CentralPaciente(int id)
        {
            var paciente = await _pacienteService.ObterPorId(id);

            if (paciente == null)
                return NotFound();

            var response = new ResponseViewModel<PacienteViewModel>
            {
                Sucesso = true,
                StatusCode = 200,
                Mensagem = "Paciente encontrado com sucesso.",
                Itens = new List<PacienteViewModel> { paciente }
            };

            return View(response);
        }


        #region Métodos auxiliares
        private async Task CriarViewBags()
        {
            await CriarViewConvenios();
        }

        private async Task CriarViewConvenios()
        {
            var resultado = await _convenioService.ListarConvenios();

            ViewBag.Convenios = resultado
                .OrderBy(x => x.NomeConvenio)
                .Select(x => new SelectListItem
                {
                    Text = x.NomeConvenio,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        #endregion


    }

}

