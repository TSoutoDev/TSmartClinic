using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IPacienteService : IBaseService<Paciente>
    {
        Task<List<Paciente>> BuscarPacientesHeader(string termo);
    }
}
