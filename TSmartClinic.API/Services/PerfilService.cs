using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.API.Services
{
    public class PerfilService : BaseService<Perfil>, IPerfilService
    {
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly INichoRepository _nichoRepository;
        private readonly IOperacaoPerfilRepository _operacaoPerfilRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly TSmartClinicContext _dbContext;
        public PerfilService(TSmartClinicContext dbContext, IOperacaoPerfilRepository operacaoPerfilRepository, IUsuarioLogadoService usuarioLogadoService, INichoRepository nichoRepository, IPerfilRepository perfilRepository) : base(perfilRepository)
        {
            _nichoRepository = nichoRepository;
            _usuarioLogadoService = usuarioLogadoService;
            _operacaoPerfilRepository = operacaoPerfilRepository;
            _perfilRepository = perfilRepository;
            _dbContext = dbContext;
        }

        public override Perfil Inserir(Perfil entity)
        {
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                if (!_usuarioLogadoService.ClienteId.HasValue)
                    throw new UnauthorizedAccessException("Não foi possível identificar o cliente do usuário.");

                var clienteId = _usuarioLogadoService.ClienteId.Value;
                var cliente = _dbContext.Cliente.FirstOrDefault(x => x.Id == clienteId);

                if (cliente == null)
                    throw new UnauthorizedAccessException("Cliente do usuário não encontrado.");

                if (!cliente.NichoId.HasValue)
                    throw new UnauthorizedAccessException("A área de atuação do cliente não está definida.");

                entity.ClienteId = clienteId;
                entity.NichoId = cliente.NichoId.Value;
            }

            return base.Inserir(entity);
        }

        public override Perfil Atualizar(Guid publicId, Perfil entity)
        {
            var perfilExistente = _perfilRepository.ObterPorPublicId(publicId);

            if (perfilExistente == null)
                throw new NotFoundException();

            // Recupera o Id interno através do PublicId
            entity.Id = perfilExistente.Id;

            if (!_usuarioLogadoService.UsuarioMaster)
            {
                entity.NichoId = _usuarioLogadoService.NichoClienteId;
                entity.ClienteId = _usuarioLogadoService.ClienteId;
            }
            // Chama o repositório, que já trata OperacaoPerfis corretamente
            var perfilAtualizado = _perfilRepository.Atualizar(entity);

            return perfilAtualizado;
        }

        public async Task<List<Perfil>> ListarPerfilPorCliente(int clienteId)
        {
            if (clienteId == 1)
            {
                return await _perfilRepository.ListarTodos();
            }

            return await _perfilRepository.ListarPerfilPorCliente(clienteId);
        }
    }
}
