using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class NichoService : BaseService<Nicho>, INichoService
    {
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly INichoRepository _nichoRepository;

        public NichoService(IUsuarioLogadoService usuarioLogadoService, INichoRepository nichoRepository) : base(nichoRepository)
        {
            _nichoRepository = nichoRepository;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Nicho>> ListarNichos()
        {
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                if (!_usuarioLogadoService.NichoClienteId.HasValue)
                    return new List<Nicho>();

                // Busca da sessão o id do nicho 
                var idNicho = _usuarioLogadoService.NichoClienteId.Value;

                // Busca apenas o nicho do usuário
                var nicho = _nichoRepository.ObterPorId(idNicho);

                // Retorna lista com apenas esse nicho
                return nicho != null
                    ? new List<Nicho> { nicho }
                    : new List<Nicho>();
            }
            // Se for Master, retorna todos
            return await _nichoRepository.ListarNichos(); 
        }
    }
}
