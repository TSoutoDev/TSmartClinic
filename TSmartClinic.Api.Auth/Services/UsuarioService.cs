using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.Api.Auth.Services
{

    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository? _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Bloquear(Guid publicId)
        {
            var usuario = _usuarioRepository?.ObterPorPublicId(publicId);

            usuario.Bloquear();

            _usuarioRepository?.Atualizar(usuario);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepository?.ObterPorEmail(email);
        }

        public void DefinirSenha(string token, string novaSenha)
        {
            throw new NotImplementedException();
        }

        public string GerarTokenResetSenha(string email)
        {
            throw new NotImplementedException();
        }

        public List<string> ObterPermissoesPorPerfil(int perfilId)
        {
            return _usuarioRepository.ObterPermissoesPorPerfil(perfilId);
        }
    }
}
