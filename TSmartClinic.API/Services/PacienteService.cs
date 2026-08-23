using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class PacienteService : BaseService<Paciente>, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository) : base(pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public Paciente ObterPorIdCliente(int idPaciente, int clienteId)
        {
            return _pacienteRepository.ObterPorIdCliente(idPaciente, clienteId);
        }

        public List<Paciente> ListarPorCliente(BaseFiltro filtro, int clienteId, params Expression<Func<Paciente, object>>[] properties)
        {
           return _pacienteRepository.ListarPorCliente(filtro, clienteId, properties);
        }
    }
}
