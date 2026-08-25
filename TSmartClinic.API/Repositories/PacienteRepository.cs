using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(TSmartClinicContext TSmartClinicContext, IUsuarioLogadoService usuarioLogadoService) : base(TSmartClinicContext, usuarioLogadoService)
        {
        }

        public Paciente ObterPorIdCliente(int idPaciente, int clienteId)
        {
            var query = _dbSet?
                .AsNoTracking()
                .Include(c => c.Convenio)
                .FirstOrDefault(x =>
                    x.Id == idPaciente && x.ClienteId == clienteId);

            return query;
        }

        public List<Paciente> ListarPorCliente(BaseFiltro filtro, int clienteId, params Expression<Func<Paciente, object>>[] properties)
        {
            var filtroPaciente = filtro as BaseFiltro;

            var query = MontarFiltro(filtro, properties);

            query = query
                .Where(x => x.ClienteId == clienteId)//proteção: retorna apenas o paciente da clinica
                .Include(x => x.Convenio);


            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroPaciente?.Nome))
            {
                var nome = filtroPaciente.Nome.Trim().ToUpper();
                query = query.Where(c => EF.Functions.ILike(c.NomePaciente, $"%{filtroPaciente.Nome.Trim()}%"));
            }

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
