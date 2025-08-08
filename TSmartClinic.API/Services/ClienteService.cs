using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<List<Cliente>> ListarClientes()
        {
            return _clienteRepository.ListarClientes();
        }
    }
}
