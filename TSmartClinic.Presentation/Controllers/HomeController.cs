using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
