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
        private readonly ICriptografiaProvider _criptografiaProvider;
       // private readonly IUsuarioClinicaPerfilRepository _usuarioClinicaPerfilRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, /*IUsuarioClinicaPerfilRepository usuarioClinicaPerfilRepository, */ICriptografiaProvider criptografiaProvider = null) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _criptografiaProvider = criptografiaProvider;
           // _usuarioClinicaPerfilRepository = usuarioClinicaPerfilRepository;

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

        public override Usuario Inserir(Usuario usuario)
        {
            usuario.Senha = _criptografiaProvider.Criptografar(usuario.Senha);

           // _usuarioClinicaPerfilRepository.Inserir()

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

            return base.Atualizar(id, usuario);
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, List<Clinica> clinicasUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
