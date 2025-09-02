using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Presentation.Models;
using TSmartClinic.Presentation.ViewModels.Filters;

namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IUsuarioService : IBaseService<UsuarioFilterViewModel,UsuarioViewModel >
    {
        Task<ResponseViewModel<UsuarioViewModel>> ObterPorEmail(string email);
        Task Bloquear(int id);
        Task<UsuarioViewModel> PreencherDados(UsuarioViewModel viewModel);
        Task ProcessarFotoAsync(UsuarioViewModel model, string foto);
        string GerarLogin();
        string GerarSenha();
        Task<ResponseViewModel<PrimeiroAcessoViewModel>> DefinirSenhaPrimeiroAcesso(int usuarioId, string novaSenha);
    }
}
