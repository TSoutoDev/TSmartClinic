using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class ConvenioRepository : BaseRepository<Convenio>, IConvenioRepository
    {
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        public ConvenioRepository(TSmartClinicContext context, IUsuarioLogadoService usuarioLogadoService) : base(context)
        {
            _usuarioLogadoService = usuarioLogadoService;
        }

        public override List<Convenio> Listar( BaseFiltro filtro,  params Expression<Func<Convenio, object>>[] properties)
        {
            var query = MontarFiltro(filtro, properties);

            var clienteId = _usuarioLogadoService.ClienteId;

            query = query.Where(x => x.ClienteId == clienteId);

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                var nome = filtro.Nome.Trim();

                query = query.Where(x => x.NomeConvenio != null && EF.Functions.ILike(x.NomeConvenio, $"%{nome}%"));
            }

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;

                query = query
                    .Skip(pagina * filtro.ItensPorPagina)
                    .Take(filtro.ItensPorPagina);
            }

            return query.ToList();
        }
    }
}
