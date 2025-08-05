using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class PerfisController : BaseController<IPerfilService,BaseFilterViewModel, PerfilViewModel>
    {
        public PerfisController(IPerfilService service) : base(service)
        {
        }


        //private async Task CriarViewBagNichos()
        //{
        //    var resultado = await _estadoService.Listar(new());
        //    var ufs = resultado.Itens.OrderBy(x => x.Uf);

        //    ViewBag.UFs = ufs
        //        .Select(x => new SelectListItem
        //        {
        //            Text = x.Uf,
        //            Value = x.Id.ToString()
        //        });
        //}
    }
}
