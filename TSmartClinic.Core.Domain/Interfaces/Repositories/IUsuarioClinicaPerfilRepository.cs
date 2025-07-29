using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioClinicaPerfilRepository : IBaseRepository<UsuarioClinicaPerfil>
    {
        Clinica ObterClinicaPadraoDoUsuario(int usuarioId);
        List<Clinica> ObterClinicasDoUsuario(int usuarioId);
    }
}
