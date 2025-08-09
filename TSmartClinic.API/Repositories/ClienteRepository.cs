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

        public async Task<List<Cliente>> ListarClientes(int? clienteId = null)
        {
            IQueryable<Cliente> query = _dbSet;

            if (clienteId.HasValue)
            {
                query = query.Where(c => c.Id == clienteId.Value).OrderBy(c => c.NomeCliente);
            }

            return await query.ToListAsync();
        }

    }
}
