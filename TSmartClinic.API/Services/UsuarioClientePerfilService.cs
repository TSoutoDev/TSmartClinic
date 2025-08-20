using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class UsuarioClientePerfilService : IUsuarioClientePerfilService
    {

        public Cliente ObterClinicaPadrao(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId)
        {
            throw new NotImplementedException();
        }
    }
}
