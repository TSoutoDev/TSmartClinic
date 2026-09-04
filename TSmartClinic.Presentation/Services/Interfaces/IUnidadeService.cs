using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IUnidadeService : IBaseService<BaseFilterViewModel, UnidadeViewModel>
    {
        Task<List<UnidadeViewModel>> ListarPorCliente(int clienteId);
    }
}