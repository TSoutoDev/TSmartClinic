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

        public void Bloquear(int id)
        {
            var usuario = _usuarioRepository?.ObterPorId(id);

            usuario.Bloquear();

            _usuarioRepository?.Atualizar(usuario);
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, int clinicaId, int moduloId)
        {
            return _usuarioRepository.ObterPermissaoUsuario(usuarioId, clinicaId, moduloId);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepository?.ObterPorEmail(email);
        }
    }
}
