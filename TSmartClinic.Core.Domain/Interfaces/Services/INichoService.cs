using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface INichoService : IBaseService<Nicho>
    {
        Task<List<Nicho>> ListarNichos();
    }
}
