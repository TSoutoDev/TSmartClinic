using AgendaApp.Core.Domain.Entities;
using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Interfaces.Services;
using AgendaApp.Core.Domain.Service;

namespace AgendaApp.API.Services
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
            var usuario = _usuarioRepository.ObterPorId(id);

            usuario.Bloquear();

            _usuarioRepository.Atualizar(usuario);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepository.ObterPorEmail(email);
        }
    }
}
