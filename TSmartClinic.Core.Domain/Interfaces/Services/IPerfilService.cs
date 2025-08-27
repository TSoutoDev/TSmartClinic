using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IPerfilService : IBaseService<Perfil>
    {
        Task<List<Perfil>> ListarPerfilPorCliente(int clienteId);
    }
}
