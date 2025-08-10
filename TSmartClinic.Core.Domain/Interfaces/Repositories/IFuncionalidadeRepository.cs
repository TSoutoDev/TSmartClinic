using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IFuncionalidadeRepository : IBaseRepository<Funcionalidade>
    {
        Task<List<Funcionalidade>> ListarFuncionalidades();
    }
}
