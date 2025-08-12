using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models.PermissoesAcesso;
using TSmartClinic.Presentation.ViewModels.Filters;
using static TSmartClinic.Presentation.Models.PermissoesAcesso.PermissoesViewModel;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IPerfilPermissaoService : IBaseService<BaseFilterViewModel, PermissoesViewModel>
    {
        Task<List<ModuloViewModel>> ListarArvorePermissoesAsync(); 
        Task<List<PermissoesViewModel>> ListarArvoreModuloPermissoesAsync(); 
        Task<List<int>> ObterOperacoesDoPerfilAsync(int perfilId);
        Task SalvarOperacoesDoPerfilAsync(int perfilId, IEnumerable<int> operacaoIds);

    }
}
