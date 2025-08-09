using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IModuloRepository : IBaseRepository<Modulo>
    {
        Task<List<Modulo>> ListarModulos();
    }
}
