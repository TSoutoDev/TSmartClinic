using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class NichoService : BaseService<Nicho>, INichoService
    {
        private readonly INichoRepository _nichoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NichoService(IHttpContextAccessor httpContextAccessor, INichoRepository nichoRepository) : base(nichoRepository)
        {
            _nichoRepository = nichoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Nicho>> ListarNichos()
        {
            var tipoUsuario = _httpContextAccessor.HttpContext?.User.FindFirst("TipoUsuario")?.Value;

            if (tipoUsuario?.ToLower() != "master")
                throw new UnauthorizedAccessException("Apenas usuários do tipo master podem acessar os nichos.");

            return await _nichoRepository.ListarNichos();
        }
    }
}
