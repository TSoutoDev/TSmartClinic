using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        void ExcluirComEnderecos(Paciente paciente);
        Task<List<Paciente>> BuscarPacientesHeader(string termo, IEnumerable<int>? clienteIds = null);
    }
}
