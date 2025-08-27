using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IPerfilRepository : IBaseRepository<Perfil>
    {
        Task<List<Perfil>> ListarPerfilPorCliente(int clienteId);
        Task <List<Perfil>> ListarTodos();
    }
}
