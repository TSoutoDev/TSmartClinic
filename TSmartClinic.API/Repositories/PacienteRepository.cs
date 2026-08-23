using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(TSmartClinicContext TSmartClinicContext) : base(TSmartClinicContext)
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
            var query = MontarFiltro(filtro, properties);
            query = query
                .Where(x => x.ClienteId == clienteId)//proteção: retorna apenas o paciente da clinica
                .Include(x => x.Convenio);

            return query.ToList();
        }
    }
}
