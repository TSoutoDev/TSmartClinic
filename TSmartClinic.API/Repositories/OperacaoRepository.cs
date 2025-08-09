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
        public List<string> ObterPermissaoUsuario(int perfilId)
        {
            throw new NotImplementedException();
        }
    }
}
