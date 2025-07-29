using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.Api.Auth.Repositories
{
    public class UsuarioClinicaPerfilRepository : BaseRepository<UsuarioClinicaPerfil>, IUsuarioClinicaPerfilRepository
    {


        private readonly TSmartClinicContext _tsmartClinicContext;

        public UsuarioClinicaPerfilRepository(TSmartClinicContext tsmartClinicContext) : base(tsmartClinicContext)
        {
            _tsmartClinicContext = tsmartClinicContext;
        }

        public Clinica ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            return _tsmartClinicContext.UsuarioClinicaPerfil
                .Where(uc => uc.Id == usuarioId && uc.ClinicaPadrao)
                .Select(uc => uc.Clinica)
                .FirstOrDefault();
        }

        public List<Clinica> ObterClinicasDoUsuario(int usuarioId)
        {
            return _tsmartClinicContext.UsuarioClinicaPerfil
                 .Where(uc => uc.Id == usuarioId)
                 .Select(uc => uc.Clinica)
                 .ToList();
        }
     
    }
}

