using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class OperacaoRepository : BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }

        async Task<List<Operacao>> IOperacaoRepository.ListarOperacoes()
        {
            return await _dbSet
                .OrderBy(x => x.NomeOperacao)
                .ToListAsync();
        }

        public List<string> ObterPermissaoUsuario(int perfilId)
        {
            throw new NotImplementedException();
        }
    }
}
