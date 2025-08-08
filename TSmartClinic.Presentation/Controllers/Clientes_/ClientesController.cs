using Microsoft.AspNetCore.Mvc;

namespace TSmartClinic.Presentation.Controllers.Clientes_
{
    public class ClientesController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
    }
}
