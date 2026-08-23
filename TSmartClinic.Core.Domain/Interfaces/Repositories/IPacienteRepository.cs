using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        Paciente ObterPorIdCliente(int idPaciente, int clienteId);
        List<Paciente> ListarPorCliente(BaseFiltro filtro, int clienteId, params Expression<Func<Paciente, object>>[] properties);
    }
}
