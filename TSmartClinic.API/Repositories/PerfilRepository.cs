using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }

        public override Perfil ObterPorId(int id, params Expression<Func<Perfil, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Perfil>;

            query = query?.Where(x => (int)x.Id == id);

            query = query?
                .Include(x => x.Nicho)?
                .Include(x => x.Cliente)
                .Include(x => x.OperacaoPerfis);
                

            var perfil = query?.FirstOrDefault();

            return perfil;

        }

        public override List<Perfil> Listar(BaseFiltro filtro, params Expression<Func<Perfil, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Perfil>;

            var filtroPerfil = filtro as BaseFiltro;

            query = MontarFiltro(filtro, properties);

            query = query
                .Include(x => x.Nicho)
                .Include(x => x.Cliente);
               // .Include(x => x.OperacaoPerfis); 

            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroPerfil.Nome))
                query = query.Where(c => c.NomePerfil.ToUpper().Contains(filtroPerfil.Nome));

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;
                query = query.Skip(pagina * filtro.ItensPorPagina)
                             .Take(filtro.ItensPorPagina);
            }

            return query.ToList();

        }

        public override Perfil Atualizar(Perfil entity)
        {
            var query = _dbSet as IQueryable<Perfil>;
            query
                .Include(x => x.Cliente)
                .Include(x => x.OperacaoPerfis); 


            return base.Atualizar(entity);
        }

        public override Perfil Inserir(Perfil entity)
        {
            var query = _dbSet as IQueryable<Perfil>;
            query
                .Include(x => x.Cliente)
                .Include(x => x.OperacaoPerfis);

            return base.Inserir(entity);
        }
    }
}

