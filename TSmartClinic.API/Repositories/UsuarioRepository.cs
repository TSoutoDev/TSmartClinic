using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TSmartClinicContext TSmartClinicContext) : base(TSmartClinicContext)
        {
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, List<Clinica> clinicasUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario ObterPorEmail(string email)
        {
            var query = _dbSet as IQueryable<Usuario>;

            return query?.FirstOrDefault(x => x.Email == email);
        }
    }
}
