using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TSmartClinic.API.Repositories
{
    public class FuncionalidadeRepository : BaseRepository<Funcionalidade>, IFuncionalidadeRepository
    {

        public FuncionalidadeRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }

       public async Task<List<Funcionalidade>> ListarFuncionalidades()
        {
            return await _dbSet
                .OrderBy(x => x.NomeFuncionalidade)
                .ToListAsync();
        }
    }
}
