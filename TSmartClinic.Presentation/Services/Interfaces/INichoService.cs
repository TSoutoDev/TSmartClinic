using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface INichoService : IBaseService<BaseFilterViewModel, NichoViewModel>
    {
        Task<List<NichoViewModel>> ListarNichos();
    }
}
