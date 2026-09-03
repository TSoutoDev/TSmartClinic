using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.Data.Repositories
{
    public class UsuarioUnidadePerfilRepository : IUsuarioUnidadePerfilRepository
    {
        private readonly TSmartClinicContext _dbContext;

        public UsuarioUnidadePerfilRepository(TSmartClinicContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UsuarioUnidadePerfil> ObterListaPorUsuarioId(int usuarioId)
        {
            return _dbContext.UsuarioUnidadePerfil
                .AsNoTracking()
                .Include(x => x.Unidade)
                .Include(x => x.Perfil)
                .Where(x => x.UsuarioId == usuarioId)
                .ToList();
        }

        public List<Unidade> ObterUnidadesDoUsuario(int usuarioId)
        {
            return _dbContext.Unidade.AsNoTracking()
                .Include(x => x.Cliente)
                .Where(u => _dbContext.UsuarioUnidadePerfil
                .Any(x => x.UsuarioId == usuarioId && x.UnidadeId == u.Id))
                .OrderBy(x => x.NomeUnidade)
                .ToList();
        }

        public Unidade? ObterUnidadePadraoDoUsuario(int usuarioId)
        {
            return _dbContext.UsuarioUnidadePerfil
                .AsNoTracking()
                .Where(x => x.UsuarioId == usuarioId && x.UnidadePadrao)
                .Select(x => x.Unidade)
                .FirstOrDefault();
        }

        public bool UsuarioPossuiAcessoUnidade(int usuarioId, int unidadeId)
        {
            return _dbContext.UsuarioUnidadePerfil
                .AsNoTracking()
                .Any(x => x.UsuarioId == usuarioId && x.UnidadeId == unidadeId);
        }

        public int? ObterPerfilIdPorUsuarioUnidade(int usuarioId, int unidadeId)
        {
            return _dbContext.UsuarioUnidadePerfil
                .AsNoTracking().Where(x => x.UsuarioId == usuarioId && x.UnidadeId == unidadeId)
                .Select(x => (int?)x.PerfilId)
                .FirstOrDefault();
        }
        public void DefinirUnidadePadrao(int usuarioId, int unidadeId)
        {
            var vinculos = _dbContext.UsuarioUnidadePerfil
                .Where(x => x.UsuarioId == usuarioId)
                .ToList();

            if (!vinculos.Any(x => x.UnidadeId == unidadeId))
                throw new InvalidOperationException("Usuário não possui acesso à unidade informada.");

            foreach (var vinculo in vinculos)
                vinculo.UnidadePadrao = vinculo.UnidadeId == unidadeId;

            _dbContext.SaveChanges();
        }

    }
}