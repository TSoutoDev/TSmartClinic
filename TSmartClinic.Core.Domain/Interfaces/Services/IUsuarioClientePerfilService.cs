using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioClientePerfilService 
    {
        Cliente ObterClinicaPadrao(int usuarioId);
        List<Cliente> ObterClinicasDoUsuario(int usuarioId);
        bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId);
    }
}
