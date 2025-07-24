using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<ResponseViewModel<AccountViewModel>> Logar(AccountViewModel accountViewModel);
        Task<ResponseViewModel<AccountViewModel>> Logout();
    }
}
