using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class OperacaoRepository : BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(TSmartClinicContext dbContext, IUsuarioLogadoService usuarioLogadoService) : base(dbContext, usuarioLogadoService)
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
