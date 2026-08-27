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

        [HttpGet]
        public override async Task<IActionResult> Cadastro(Guid? publicId)
        {
            await CriarViewBags();

            return await base.Cadastro(publicId);
        }

        [HttpPost]
        public override async Task<IActionResult> Cadastro(PacienteViewModel model)
        {
            var arquivo = Request.Form.Files["FotoArquivo"];

            if (arquivo != null && arquivo.Length > 0)
            {
                using var memoryStream = new MemoryStream();

                await arquivo.CopyToAsync(memoryStream);

                model.Foto = memoryStream.ToArray();
                model.FotoContentType = arquivo.ContentType;
            }
            else if (model.PublicId.HasValue)
            {
                var pacienteAtual = await _pacienteService.ObterPorPublicId(model.PublicId.Value);

                if (pacienteAtual?.Sucesso == true &&
                    pacienteAtual.Itens != null &&
                    pacienteAtual.Itens.Any())
                {
                    model.Foto = pacienteAtual.Itens.First().Foto;
                    model.FotoContentType = null;
                }
            }

            var result = await base.Cadastro(model);

            await CriarViewBags();

            return result;
        }

        [HttpPost]
        public override async Task<IActionResult> BuscaAvancada(BaseFilterViewModel filtro)
        {
            return await base.BuscaAvancada(filtro);
        }

        [HttpGet]
        public async Task<IActionResult> CentralPaciente(Guid publicId)
        {
            var retorno = await _pacienteService.ObterPorPublicId(publicId);

            if (!retorno.Sucesso || retorno.Itens == null || !retorno.Itens.Any())
                return NotFound();

            var paciente = retorno.Itens.First();

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

