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
               .Where(n => n.Id != 0)         // filtra os nichos inválidos
               .OrderBy(x => x.NomeNicho)     // ordena pelo nome
               .ToListAsync();

            return response;
        }
    }
}
