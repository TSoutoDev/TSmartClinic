using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUnidadeService : IBaseService<Unidade>
    {
        List<Unidade> ListarPorCliente(int clienteId);
    }
}