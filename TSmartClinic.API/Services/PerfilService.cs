using TSmartClinic.API.Repositories;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class PerfilService : BaseService<Perfil>, IPerfilService
    {
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly INichoRepository _nichoRepository;
        public PerfilService(IUsuarioLogadoService usuarioLogadoService, INichoRepository nichoRepository, IPerfilRepository perfilRepository) : base(perfilRepository)
        {
            _nichoRepository = nichoRepository;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public override Perfil Inserir(Perfil entity)
        {
            if (!_usuarioLogadoService.UsuarioMaster)
                throw new UnauthorizedAccessException("Apenas usuários do tipo master podem acessar os nichos.");


          //  var clienteId = _usuarioLogadoService.ClienteId


            return base.Inserir(entity);
        }

        public Task<List<Nicho>> ListarNichos()
        {
            throw new NotImplementedException();
        }
    }
}
