using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Cliente>> ListarClientes()
        {
            return await _dbSet
                .OrderByDescending(c => c.NomeCliente).ToListAsync();
        }

    }
}
