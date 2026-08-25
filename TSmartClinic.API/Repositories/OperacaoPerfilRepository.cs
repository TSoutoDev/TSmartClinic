using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class OperacaoPerfilRepository : BaseRepository<OperacaoPerfil>, IOperacaoPerfilRepository
    {
        public OperacaoPerfilRepository(TSmartClinicContext dbContext, IUsuarioLogadoService usuarioLogadoService) : base(dbContext, usuarioLogadoService  )
        {
        }

        // Agora não chama SaveChanges — apenas adiciona ao ChangeTracker
        public void AdicionarRange(IEnumerable<OperacaoPerfil> operacaoPerfis)
        {
            if (operacaoPerfis == null) return;
            _dbContext.Set<OperacaoPerfil>().AddRange(operacaoPerfis);
        }

        public List<OperacaoPerfil> ListaOperacoPerfilPorPerfil(int id)
        {
            var query = _dbSet as IQueryable<OperacaoPerfil>;
            query = query?.Where(x => (int)x.PerfilId == id);
            return query?.ToList();
        }

        //public void RemoverPorPerfilId(int perfilId)
        //{
        //    var sql = "DELETE FROM OperacaoPerfil WHERE PerfilId = @perfilId";
        //    _dbContext.Database.ExecuteSqlRaw(sql, new PostgrePar("@perfilId", perfilId));
        //}
        public void RemoverPorPerfilId(int perfilId)
        {
            _dbContext.Set<OperacaoPerfil>()
                .Where(op => op.PerfilId == perfilId)
                .ExecuteDelete(); // traduz para DELETE ... WHERE ...
        }
    }
}
