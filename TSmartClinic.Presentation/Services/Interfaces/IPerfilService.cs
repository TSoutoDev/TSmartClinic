using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IPerfilService : IBaseService<BaseFilterViewModel, PerfilViewModel>
    {
        Task<List<PerfilViewModel>> ListarPerfilPorCliente(int clienteId);
    }
}
