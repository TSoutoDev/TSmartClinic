using AgendaApp.Core.Domain.Entities;

namespace AgendaApp.Core.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario ObterPorEmail(string email);
        void Bloquear(int id);
        List<string> ObterPermissaoUsuario(int usuarioId, int sistemaId);
    }
}
