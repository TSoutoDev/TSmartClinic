using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IFuncionalidadeService : IBaseService<Funcionalidade>
    {
        Task<List<Funcionalidade>> ListarFuncionalidades();
    }
}
