using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class UnidadeRepository : BaseRepository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(TSmartClinicContext context, IUsuarioLogadoService usuarioLogadoService) : base(context, usuarioLogadoService)
        {
        }

        public List<Unidade> ListarPorCliente(int clienteId)
        {
            return _dbSet
                .AsNoTracking()
                .Include(x => x.Cliente)
                .Where(x => x.ClienteId == clienteId && x.Ativo)
                .OrderBy(x => x.NomeUnidade)
                .ToList();
        }
    }
}