using Microsoft.Identity.Client;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.Api.Auth.Repositories
{
    public class OperacaoRepository : BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(TSmartClinicContext TSmartClinicContext) : base(TSmartClinicContext)
        { 
        }

        public Task<List<Operacao>> ListarOperacoes()
        {
            throw new NotImplementedException();
        }

        public List<string> ObterPermissaoUsuario(int perfilId)
        {
            var usuario = _dbSet as IQueryable<Usuario>;

            var permissoes = usuario.ToList();

            return new List<string>();
        }
    }
}
