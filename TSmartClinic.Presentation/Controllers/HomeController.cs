using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Settings;

namespace TSmartClinic.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<UrlApiSettings>? _urlApiSettings;
        public HomeController(IOptions<UrlApiSettings>? urlApiSettings)
        {
            _urlApiSettings = urlApiSettings;
        }
        public IActionResult Index()
        {
            ViewBag.APIGateway = _urlApiSettings.Value.ApiGateway;

            var model = new DashboardViewModel
            {
                TotalAcoes = 25,
                AcoesAtivas = 18,
                AcoesInativas = 7,
                UltimasAcoes = new List<string>
        {
            "Inspeção de segurança - Almoxarifado",
            "Capacitação em NR-10",
            "Revisão de EPIs - Equipe A",
            "Auditoria interna de SST",
            "Campanha de prevenção contra acidentes"
        },
                Meses = new List<string> { "Fev", "Mar", "Abr", "Mai", "Jun", "Jul" },
                AcoesPorMes = new List<int> { 2, 3, 4, 3, 5, 8 }
            };
            return View(model); // Vai para Views/Home/Index.cshtml
   
        }
    }
}
