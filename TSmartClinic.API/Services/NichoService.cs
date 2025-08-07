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
                throw new UnauthorizedAccessException("Apenas usuários do tipo master podem acessar os nichos.");

          
            return await _nichoRepository.ListarNichos();
        }
    }
}
