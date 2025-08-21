using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.Api.Auth.Services
{
    public class UsuarioClinicaPerfilService : IUsuarioClientePerfilService
    {
        private readonly IUsuarioClientePerfilRepository _usuarioClinicaPerfilRepository;

        public UsuarioClinicaPerfilService(IUsuarioClientePerfilRepository usuarioClinicaPerfilRepository) 
        {
            _usuarioClinicaPerfilRepository = usuarioClinicaPerfilRepository;
        }

        public void ExluirPorUsuarioId(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Cliente ObterClinicaPadrao(int usuarioId)
        {
            return _usuarioClinicaPerfilRepository.ObterClinicaPadraoDoUsuario(usuarioId);
        }

        public Cliente ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            return _usuarioClinicaPerfilRepository.ObterClinicasDoUsuario(usuarioId);
        }

        public List<UsuarioClientePerfil> ObterListaPorUsuarioId(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId)
        {
            var clinicas = _usuarioClinicaPerfilRepository.ObterClinicasDoUsuario(usuarioId);
            return clinicas.Any(c => c.Id == clinicaId);
        }
    }
}
