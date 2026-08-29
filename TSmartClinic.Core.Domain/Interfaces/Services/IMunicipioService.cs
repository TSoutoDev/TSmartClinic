using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IMunicipioService
    {
        Task<Municipio?> ObterPorId(int id);
    }
}
