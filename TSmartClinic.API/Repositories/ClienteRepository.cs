using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(TSmartClinicContext dbContext, IUsuarioLogadoService usuarioLogadoService) : base(dbContext, usuarioLogadoService)
        {
        }
        public override Cliente ObterPorId(int id, params Expression<Func<Cliente, object>>[] properties)
        {
            var query = _dbSet
                .Include(x => x.Nicho)
                .Include(x => x.ClienteEndereco)
                    .ThenInclude(x => x.Endereco)
                .AsQueryable();

            return query.FirstOrDefault(x => x.Id == id);
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

        public override Cliente ObterPorPublicId(Guid publicId, params Expression<Func<Cliente, object>>[] properties)
        {
            var query = _dbSet
                .Include(x => x.Nicho)
                .Include(x => x.ClienteEndereco)
                    .ThenInclude(x => x.Endereco)
                .AsQueryable();

            query = AplicarFiltroCliente(query);

            return query.FirstOrDefault(x => x.PublicId == publicId);
        }

        public void ExcluirComEnderecos(Cliente cliente)
        {
            var enderecos = cliente.ClienteEndereco?
                .Where(x => x.Endereco != null)
                .Select(x => x.Endereco!)
                .ToList();

            _dbSet?.Remove(cliente);

            if (enderecos != null && enderecos.Any())
            {
                _dbContext?.Set<Endereco>().RemoveRange(enderecos);
            }

            _dbContext?.SaveChanges();
        }

        public override List<Cliente> Listar(BaseFiltro filtro, params Expression<Func<Cliente, object>>[] properties)
        {
            var query = MontarFiltro(filtro, properties);

            query = AplicarFiltroCliente(query);

            query = query
                .Include(x => x.Nicho)
                .Include(x => x.ClienteEndereco)
                    .ThenInclude(x => x.Endereco);

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                var nome = filtro.Nome.Trim();
                query = query.Where(x => x.NomeCliente != null && EF.Functions.ILike(x.NomeCliente, $"%{nome}%"));
            }

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;

                query = query
                    .Skip(pagina * filtro.ItensPorPagina)
                    .Take(filtro.ItensPorPagina);
            }

            return query.ToList();
        }
    }
}
