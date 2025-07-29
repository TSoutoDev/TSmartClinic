using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioClinicaPerfilService : IBaseService<UsuarioClinicaPerfil>
    {
        Clinica ObterClinicaPadrao(int usuarioId);
        List<Clinica> ObterClinicasDoUsuario(int usuarioId);
        bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId);
    }
}
