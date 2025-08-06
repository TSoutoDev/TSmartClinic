using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class PerfisController : BaseController<IPerfilService,BaseFilterViewModel, PerfilViewModel>
    {
        private readonly IPerfilService _perfilService;
        public PerfisController(IPerfilService perfilService) : base(perfilService)
        {
            _perfilService = perfilService;
        }


        public override async Task<IActionResult> Cadastro(PerfilViewModel model)
        {
            await CriarViewBags();
            return await base.Cadastro(model);
        }

        public override async Task<IActionResult> Cadastro(int? id)
        {
            await CriarViewBags();
            return await base.Cadastro(id);
        }

        private async Task CriarViewBags()
        {
            await CriarViewBagNicho();
         //   await CriarViewBagRiscos();
         //   await CriarViewBagFatoresRiscos();
        }

        private async Task CriarViewBagNicho()
        {

            var resultado = await _perfilService.ListarNichos();

            ViewBag.Nichos = resultado
                .Select(x => new SelectListItem
                {
                    Text = x.NomeNicho,
                    Value = x.Id.ToString()
                });
        }

    }
}
