using AgendaApp.Core.Domain.Entities;
using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Data.Contexts;
using AgendaApp.Data.Repositories;

namespace AgendaApp.API.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AgendaAppContext AgendaAppContext) : base(AgendaAppContext)
        {
        }

    
        public Usuario ObterPorEmail(string email)
        {
            var query = _dbSet as IQueryable<Usuario>;

            return query?.FirstOrDefault(x => x.Email == email);
        }
    }
}
