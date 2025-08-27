using TSmartClinic.API.Repositories;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IPerfilRepository? _perfilRepository;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public UsuarioService(IUsuarioLogadoService usuarioLogadoService, IPerfilRepository perfilRepository, IUsuarioRepository usuarioRepository, ICriptografiaProvider criptografiaProvider = null) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _criptografiaProvider = criptografiaProvider;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public void Bloquear(int id)
        {
            var usuario = _usuarioRepository?.ObterPorId(id);

            usuario?.Bloquear();

            _usuarioRepository?.Atualizar(usuario);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepository.ObterPorEmail(email);
        }

        public override Usuario Inserir(Usuario usuario)
        {
            usuario.Senha = _criptografiaProvider.Criptografar(usuario.Senha);
            
            if (_usuarioLogadoService.UsuarioMaster)
            {
                _perfilRepository.ListarTodos();
            }

                return base.Inserir(usuario);
        }

        public override Usuario Atualizar(int id, Usuario usuario)
        {
            var usuarioExistente = _usuarioRepository?.ObterPorId(id);

            // Se a senha foi alterada, criptografar
            if (!string.Equals(usuario.Senha, usuarioExistente))
            {
                usuario.Senha = _criptografiaProvider.Criptografar(usuario.Senha);
            }
            // Atualizar o usuario
            var usuarioAtualizado = _usuarioRepository?.Atualizar(usuario);
            
            return base.Atualizar(id, usuario);
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clinicasUsuario)
        {
            throw new NotImplementedException();
        } 
    }
}
