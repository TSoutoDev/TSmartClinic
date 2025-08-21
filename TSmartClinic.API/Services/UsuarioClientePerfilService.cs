using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
   
    public class UsuarioClientePerfilService : IUsuarioClientePerfilService
    {
        private readonly IUsuarioClientePerfilRepository _usuarioClientePerfilRepository;

        public UsuarioClientePerfilService(IUsuarioClientePerfilRepository usuarioClientePerfilRepository)
        {
            _usuarioClientePerfilRepository = usuarioClientePerfilRepository;
        }

        public void ExluirPorUsuarioId(int usuarioId)
        {
            _usuarioClientePerfilRepository.ExluirPorUsuarioId(usuarioId);
        }

        public Cliente ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            return _usuarioClientePerfilRepository.ObterClinicaPadraoDoUsuario(usuarioId);
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            return _usuarioClientePerfilRepository.ObterClinicasDoUsuario(usuarioId);
        }

        public List<UsuarioClientePerfil> ObterListaPorUsuarioId(int usuarioId)
        {
            return _usuarioClientePerfilRepository.ObterListaPorUsuarioId(usuarioId);
        }

        public bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId)
        {
            return _usuarioClientePerfilRepository.UsuarioPossuiAcessoClinica(usuarioId, clinicaId);
        }
    }
}
