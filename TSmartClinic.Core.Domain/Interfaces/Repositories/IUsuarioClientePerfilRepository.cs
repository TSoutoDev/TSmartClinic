using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUsuarioClientePerfilRepository
    {
        Cliente ObterClinicaPadraoDoUsuario(int usuarioId);
        List<Cliente> ObterClinicasDoUsuario(int usuarioId);
    }
}
