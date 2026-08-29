using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.API.Repositories
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly TSmartClinicContext _dbContext;

        public MunicipioRepository(TSmartClinicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Municipio?> ObterPorId(int id)
        {
            return await _dbContext
                .Set<Municipio>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}