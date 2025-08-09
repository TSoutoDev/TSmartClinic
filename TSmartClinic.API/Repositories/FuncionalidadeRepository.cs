using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class FuncionalidadeRepository : BaseRepository<Funcionalidade>, IFuncionalidadeRepository
    {
        public FuncionalidadeRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }
    }
}
