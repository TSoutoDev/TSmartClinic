using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Service;
using AgendaApp.Data.Entities;

namespace AgendaApp.API.Services
{
    public class TarefaService : BaseService<Tarefa>
    {
        public TarefaService(IBaseRepository<Tarefa> baseRepository) : base(baseRepository)
        {
        }
    }
}
