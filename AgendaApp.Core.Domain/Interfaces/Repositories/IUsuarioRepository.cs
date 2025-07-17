using AgendaApp.Core.Domain.Entities;

namespace AgendaApp.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario ObterPorEmail(string email);
        List<string> ObterPermissaoUsuario(int usuarioId, int sistemaId);
    }
}
