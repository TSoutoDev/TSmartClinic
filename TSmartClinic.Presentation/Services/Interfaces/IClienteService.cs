using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IClienteService : IBaseService<BaseFilterViewModel, ClienteViewModel>
    {
        Task<List<ClienteViewModel>> ListarClientes();
    }
}
