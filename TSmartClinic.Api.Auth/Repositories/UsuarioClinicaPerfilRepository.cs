using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.Api.Auth.Repositories
{
    public class UsuarioClinicaPerfilRepository : BaseRepository<UsuarioClientePerfil>, IUsuarioClientePerfilRepository
    {


        private readonly TSmartClinicContext _tsmartClinicContext;

        public UsuarioClinicaPerfilRepository(TSmartClinicContext tsmartClinicContext) : base(tsmartClinicContext)
        {
            _tsmartClinicContext = tsmartClinicContext;
        }

        public Cliente ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            return _tsmartClinicContext.UsuarioClientePerfil
                .Where(uc => uc.Id == usuarioId && uc.ClientePadrao)
                .Select(uc => uc.Clinica)
                .FirstOrDefault();
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            return _tsmartClinicContext.UsuarioClientePerfil
                 .Where(uc => uc.Id == usuarioId)
                 .Select(uc => uc.Clinica)
                 .ToList();
        }
     
    }
}

