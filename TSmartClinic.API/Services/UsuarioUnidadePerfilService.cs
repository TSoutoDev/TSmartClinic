using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.Core.Domain.Services
{
    public class UsuarioUnidadePerfilService : IUsuarioUnidadePerfilService
    {
        private readonly IUsuarioUnidadePerfilRepository _repository;

        public UsuarioUnidadePerfilService(IUsuarioUnidadePerfilRepository repository)
        {
            _repository = repository;
        }

        public List<UsuarioUnidadePerfil> ObterListaPorUsuarioId(int usuarioId)
        {
            return _repository.ObterListaPorUsuarioId(usuarioId);
        }

        public List<Unidade> ObterUnidadesDoUsuario(int usuarioId)
        {
            return _repository.ObterUnidadesDoUsuario(usuarioId);
        }

        public Unidade? ObterUnidadePadraoDoUsuario(int usuarioId)
        {
            return _repository.ObterUnidadePadraoDoUsuario(usuarioId);
        }

        public bool UsuarioPossuiAcessoUnidade(int usuarioId, int unidadeId)
        {
            return _repository.UsuarioPossuiAcessoUnidade(usuarioId, unidadeId);
        }

        public int? ObterPerfilIdPorUsuarioUnidade(int usuarioId, int unidadeId)
        {
            return _repository.ObterPerfilIdPorUsuarioUnidade(usuarioId, unidadeId);
        }
    }
}