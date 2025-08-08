using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario ObterPorEmail(string email);
        List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clientesUsuario);
   
    }
}
