using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class ModuloRepository : BaseRepository<Modulo>, IModuloRepository
    {
        public ModuloRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Modulo>> ListarPermissoesAsync()
        {
            IQueryable<Modulo> query = _dbSet

             .Include(m => m.Funcionalidades)
                 .ThenInclude(f => f.Operacoes)
             .OrderBy(m => m.NomeModulo);

             var ttt =  await query.ToListAsync();

            return ttt;
        }

        public async Task<List<Modulo>> ListarModulos()
        {
            return await _dbSet
                .OrderBy(x => x.NomeModulo)
                .ToListAsync();
        }
    }
}
