using TSmartClinic.Core.Domain.Entities;
namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario ObterPorEmail(string email);
        void Bloquear(int id);
        void DefinirSenhaPrimeiroAcesso(int usuarioId, string novaSenha);
        List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clinicasUsuario);
    }
}
