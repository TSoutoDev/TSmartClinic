using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IModuloRepository : IBaseRepository<Modulo>
    {
       Task<List<Modulo>> ListarModulos();
       public Task<List<Modulo>> ListarPermissoesAsync(CancellationToken ct = default);
       public Task<List<Modulo>> ListarIdsPorPerfilAsync(int perfilId, CancellationToken ct = default);

    }
}
