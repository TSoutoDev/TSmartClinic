using TSmartClinic.Presentation.Models;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        ResponseViewModel<AccountViewModel> Logar(AccountViewModel accountViewModel);
    }
}
