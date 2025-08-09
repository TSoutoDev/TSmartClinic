using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        IClienteRepository _clienteRepository;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        public ClienteService(IUsuarioLogadoService usuarioLogadoService, IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                // Preenche o ID do cliente  que esta garava da sessão 
                var clienteId = _usuarioLogadoService.ClienteId;

                // Passa o ClienteId do usuário para filtrar
                return await _clienteRepository.ListarClientes(_usuarioLogadoService.ClienteId);

            }
            // Usuário master: retorna todos
            return await _clienteRepository.ListarClientes();
        }
    }
}
