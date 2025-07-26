using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

            return View();
        }
    }
}
