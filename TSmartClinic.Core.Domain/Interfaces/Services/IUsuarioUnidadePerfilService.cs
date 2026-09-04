    using TSmartClinic.Core.Domain.Entities;

    namespace TSmartClinic.Core.Domain.Interfaces.Services
    {
        public interface IUsuarioUnidadePerfilService
        {
            List<UsuarioUnidadePerfil> ObterListaPorUsuarioId(int usuarioId);
            List<Unidade> ObterUnidadesDoUsuario(int usuarioId);
            Unidade? ObterUnidadePadraoDoUsuario(int usuarioId);
            bool UsuarioPossuiAcessoUnidade(int usuarioId, int unidadeId);
            int? ObterPerfilIdPorUsuarioUnidade(int usuarioId, int unidadeId);
            void DefinirUnidadePadrao(int usuarioId, int unidadeId);
        }
    }