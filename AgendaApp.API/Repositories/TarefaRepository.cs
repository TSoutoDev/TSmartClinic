using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Data.Contexts;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Repositories;

namespace AgendaApp.API.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(AgendaAppContext dbContext) : base(dbContext)
        {
        }
    }
}
