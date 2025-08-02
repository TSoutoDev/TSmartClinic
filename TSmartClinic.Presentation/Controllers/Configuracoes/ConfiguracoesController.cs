using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.Presentation.Controllers.Configuracoes
{
    public class ConfiguracoesController : Controller
    {
        public IActionResult Configuracoes()
        {
            return View();
        }

    }
}
