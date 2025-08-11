using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IModuloService : IBaseService<Modulo>
    {
        Task<List<Modulo>> ListarModulos();
        public Task<List<Modulo>> ListarPermissoesAsync();
    }
}
