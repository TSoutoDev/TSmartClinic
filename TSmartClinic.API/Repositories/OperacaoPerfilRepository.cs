using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class OperacaoPerfilRepository : BaseRepository<OperacaoPerfil>, IOperacaoPerfilRepository
    {
        public OperacaoPerfilRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }
    }
}
