using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IPacienteService : IBaseService<BaseFilterViewModel, PacienteViewModel>
    {
        Task<List<PacienteViewModel>> ListarPacientes();
        Task<PacienteViewModel?> ObterPorId(int id);
    }
}
