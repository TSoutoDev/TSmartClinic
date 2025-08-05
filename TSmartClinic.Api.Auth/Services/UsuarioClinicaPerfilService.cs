using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.Api.Auth.Services
{
    public class UsuarioClinicaPerfilService : BaseService<UsuarioClientePerfil>, IUsuarioClientePerfilService
    {
        private readonly IUsuarioClientePerfilRepository _usuarioClinicaPerfilRepository;

        public UsuarioClinicaPerfilService(IUsuarioClientePerfilRepository usuarioClinicaPerfilRepository) : base(usuarioClinicaPerfilRepository)
        {
            _usuarioClinicaPerfilRepository = usuarioClinicaPerfilRepository;
        }

        public Cliente ObterClinicaPadrao(int usuarioId)
        {
            return _usuarioClinicaPerfilRepository.ObterClinicaPadraoDoUsuario(usuarioId);
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
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
