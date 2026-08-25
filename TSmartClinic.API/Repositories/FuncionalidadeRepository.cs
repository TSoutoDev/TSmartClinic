using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class FuncionalidadeRepository : BaseRepository<Funcionalidade>, IFuncionalidadeRepository
    {

        public FuncionalidadeRepository(TSmartClinicContext dbContext, IUsuarioLogadoService usuarioLogadoService) : base(dbContext, usuarioLogadoService)
        {
        }

       public async Task<List<Funcionalidade>> ListarFuncionalidades()
        {
            return await _dbSet
                .OrderBy(x => x.NomeFuncionalidade)
                .ToListAsync();
        }
    }
}
