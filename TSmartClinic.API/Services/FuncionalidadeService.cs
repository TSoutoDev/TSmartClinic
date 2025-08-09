using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class FuncionalidadeService : BaseService<Funcionalidade>, IFuncionalidadeService
    {
        public FuncionalidadeService(IFuncionalidadeRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
