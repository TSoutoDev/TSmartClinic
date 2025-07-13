using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Data.Contexts;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Repositories;

namespace AgendaApp.API.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AgendaAppContext dbContext) : base(dbContext)
        {
        }
    }
}
