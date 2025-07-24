using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IBaseService<TFilterViewModel, TViewModel>
        where TFilterViewModel : BaseFilterViewModel
        where TViewModel : BaseViewModel
    {
        Task<ResponseViewModel<TViewModel>> ObterPorId(int id);
        Task<ResponseViewModel<TViewModel>> Inserir(TViewModel entity);
        Task<ResponseViewModel<TViewModel>> Atualizar(int id, TViewModel entity);
        Task<ResponseViewModel<TViewModel>> Excluir(int id);
        Task<ResponseViewModel<TViewModel>> Listar(TFilterViewModel filtro);
    }
}
