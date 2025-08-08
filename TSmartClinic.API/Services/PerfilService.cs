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
            // Verifica se o nicho está disponível
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                if (!_usuarioLogadoService.NichoClienteId.HasValue)
                     throw new UnauthorizedAccessException("Não foi possivel acessar a area de atuação do cliente.");

                // Preenche o ID do nicho e cliente no perfil que vai ser inserido
                entity.NichoId = _usuarioLogadoService.NichoClienteId.Value;//trocar p pegar da model (tela)
                entity.ClienteId = _usuarioLogadoService.ClienteId.Value;
            }

            return base.Inserir(entity);
        }
    }
}
