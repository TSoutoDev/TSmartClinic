using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioClientePerfilService 
    {
        Cliente ObterClinicaPadraoDoUsuario(int usuarioId);
        List<Cliente> ObterClinicasDoUsuario(int usuarioId);
        bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId);
        List<UsuarioClientePerfil>ObterListaPorUsuarioId(int usuarioId);
        void ExluirPorUsuarioId(int usuarioId);
    }
}
