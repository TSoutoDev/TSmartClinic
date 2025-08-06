using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class NichoRepository : BaseRepository<Nicho>, INichoRepository
    {
        public NichoRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Nicho>> ListarNichos()
        {
           
            var response = await _dbSet
                .OrderBy(x => x.NomeNicho)
                .ToListAsync();

            return response;
        }
    }
}
