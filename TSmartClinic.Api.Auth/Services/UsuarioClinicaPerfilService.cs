using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.Api.Auth.Services
{
    public class UsuarioClinicaPerfilService : BaseService<UsuarioClinicaPerfil>, IUsuarioClinicaPerfilService
    {
        private readonly IUsuarioClinicaPerfilRepository _usuarioClinicaPerfilRepository;

        public UsuarioClinicaPerfilService(IUsuarioClinicaPerfilRepository usuarioClinicaPerfilRepository) : base(usuarioClinicaPerfilRepository)
        {
            _usuarioClinicaPerfilRepository = usuarioClinicaPerfilRepository;
        }

        public Clinica ObterClinicaPadrao(int usuarioId)
        {
            return _usuarioClinicaPerfilRepository.ObterClinicaPadraoDoUsuario(usuarioId);
        }

        public List<Clinica> ObterClinicasDoUsuario(int usuarioId)
        {
            return _usuarioClinicaPerfilRepository.ObterClinicasDoUsuario(usuarioId);
        }

        public bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId)
        {
            var clinicas = _usuarioClinicaPerfilRepository.ObterClinicasDoUsuario(usuarioId);
            return clinicas.Any(c => c.Id == clinicaId);
        }
    }
}
