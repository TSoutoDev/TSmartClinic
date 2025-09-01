using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.Services.Interfaces;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Controllers
{
    public class NichosController : BaseController<INichoService,BaseFilterViewModel, NichoViewModel>
    {
        public NichosController(INichoService service) : base(service)
        {
        }
    }
}
