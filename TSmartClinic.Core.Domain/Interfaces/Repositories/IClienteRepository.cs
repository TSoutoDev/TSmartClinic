using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<List<Cliente>> ListarClientes(int? clienteId = null);

    }
}
