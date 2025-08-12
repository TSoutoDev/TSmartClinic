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
        public Task<List<Modulo>> ListarPermissoesAsync(CancellationToken ct = default)
        {
            return _dbSet
                .AsNoTracking()
                .AsSplitQuery() // opcional, ajuda a evitar cartesian explosion
                .Include(m => m.Funcionalidades)
                    .ThenInclude(f => f.Operacoes)
                .OrderBy(m => m.NomeModulo)
                .ToListAsync(ct);
        }


        public async Task<List<Modulo>> ListarModulos()
        {
            return await _dbSet
                .OrderBy(x => x.NomeModulo)
                .ToListAsync();
        }

    }
}
