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
            IQueryable<Cliente> query = _dbSet
                .Where(c => c.Id != 0); // filtra clientes inválidos

            if (clienteId.HasValue)
            {
                query = query.Where(c => c.Id == clienteId.Value);
            }

            query = query.OrderBy(c => c.NomeCliente); // sempre ordenar

            return await query.ToListAsync();
        }

    }
}
