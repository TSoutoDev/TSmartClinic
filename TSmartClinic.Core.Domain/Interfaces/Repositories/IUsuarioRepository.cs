using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario ObterPorEmail(string email);
        void AtualizarSenhaHash(int usuarioId, string senhaHash);
        List<string> ObterPermissoesPorPerfil(int perfilId);
    }
}
