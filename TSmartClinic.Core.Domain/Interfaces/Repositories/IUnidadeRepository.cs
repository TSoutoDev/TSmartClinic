using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IUnidadeRepository : IBaseRepository<Unidade>
    {
        List<Unidade> ListarPorCliente(int clienteId);
    }
}