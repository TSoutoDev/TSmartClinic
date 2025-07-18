using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Data.Entities;

namespace TSmartClinic.API.Services
{
    public class TarefaService : BaseService<Tarefa>
    {
        public TarefaService(IBaseRepository<Tarefa> baseRepository) : base(baseRepository)
        {
        }
    }
}
