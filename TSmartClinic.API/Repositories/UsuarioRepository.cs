using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TSmartClinicContext TSmartClinicContext) : base(TSmartClinicContext)
        {
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clientesUsuario)
        {

            throw new NotImplementedException();
        }

        public Usuario ObterPorEmail(string email)
        {
            var query = _dbSet as IQueryable<Usuario>;

            return query?.FirstOrDefault(x => x.Email == email);
        }

        public override Usuario ObterPorId(int id, params Expression<Func<Usuario, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Usuario>;

            query = query?.Where(x => (int)x.Id == id);

            query = query?.Include(x => x.Cliente);

            var usuario = query?.FirstOrDefault();

            return usuario;

        }

        public override List<Usuario> Listar(BaseFiltro filtro, params Expression<Func<Usuario, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Usuario>;

            var filtroPerfil = filtro as BaseFiltro;

            query = MontarFiltro(filtro, properties);

            query = query
                .Include(x => x.Cliente);

            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroPerfil.Nome))
                query = query.Where(c => c.Nome.ToUpper().Contains(filtroPerfil.Nome));

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;
                query = query.Skip(pagina * filtro.ItensPorPagina)
                             .Take(filtro.ItensPorPagina);
            }

            return query.ToList();

        }
    }
}
