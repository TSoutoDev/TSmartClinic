using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario ObterPorEmail(string email);
        void Bloquear(int id);
        List<string> ObterPermissaoUsuario(int usuarioId, List<Clinica> clinicasUsuario);
    }
}
