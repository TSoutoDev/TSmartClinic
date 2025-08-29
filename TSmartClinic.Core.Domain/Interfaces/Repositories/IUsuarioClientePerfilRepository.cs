using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioClientePerfilRepository
    {
        Cliente ObterClinicaPadraoDoUsuario(int usuarioId);
        List<Cliente> ObterClinicasDoUsuario(int usuarioId);
        List<UsuarioClientePerfil> ObterListaPorUsuarioId(int usuarioId);
        bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId);
        void ExluirPorUsuarioId(int usuarioId);
        void AdicionarRange(IEnumerable<UsuarioClientePerfil> usuarioClientePerfis);// Adiciona uma lista de OperacaoPerfis
        void Inserir(UsuarioClientePerfil usuarioClientePerfil);

    }
}
