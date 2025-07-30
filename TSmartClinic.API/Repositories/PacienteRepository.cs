using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
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

        public override Paciente ObterPorId(int id, params Expression<Func<Paciente, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Paciente>;
            query = query?.Where(x => (int)x.Id == id);
            query = query?
                .Include(x => x.Convenio);

            var paciente = query?.FirstOrDefault();

            return paciente;
        }
    }
}
