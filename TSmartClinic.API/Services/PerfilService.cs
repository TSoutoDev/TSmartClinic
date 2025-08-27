using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TSmartClinic.API.Repositories;
using TSmartClinic.Core.Domain.Entities;
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
                if (!_usuarioLogadoService.NichoClienteId.HasValue)
                    throw new UnauthorizedAccessException("Não foi possivel acessar a area de atuação do cliente.");

                // Preenche o ID do nicho e cliente no perfil que vai ser inserido
                entity.NichoId = _usuarioLogadoService?.NichoClienteId.Value;//trocar p pegar da model (tela)
                entity.ClienteId = _usuarioLogadoService?.ClienteId.Value;
            }

            return base.Inserir(entity);
        }

        public override Perfil Atualizar(int id, Perfil entity)
        {
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
            if (clienteId == 0)
            {
                return await _perfilRepository.ListarTodos();
            }

            return await _perfilRepository.ListarPerfilPorCliente(clienteId);
        }
    }
}
