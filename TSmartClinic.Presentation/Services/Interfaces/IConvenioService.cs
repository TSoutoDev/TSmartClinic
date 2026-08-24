using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IConvenioService : IBaseService<BaseFilterViewModel, ConvenioViewModel>
    {
        Task<List<ConvenioViewModel>> ListarConvenios();
    }
}
