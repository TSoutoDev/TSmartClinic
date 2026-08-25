using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Entities;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(TSmartClinicContext TSmartClinicContext, IUsuarioLogadoService usuarioLogadoService) : base(TSmartClinicContext, usuarioLogadoService)
        {
        }
    }
}
