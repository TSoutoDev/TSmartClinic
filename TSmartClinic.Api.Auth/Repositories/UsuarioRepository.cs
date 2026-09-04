using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.Api.Auth.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly TSmartClinicContext _context;
        public UsuarioRepository(TSmartClinicContext TSmartClinicContext) : base(TSmartClinicContext)
        {
            _context = TSmartClinicContext;
        }

        public void AtualizarSenhaHash(int usuarioId, string senhaHash)
        {
            throw new NotImplementedException();
        }

        public Usuario ObterPorEmail(string email)
        {
            var query = _dbSet
               .Include(u => u.Cliente)  // Inclui o Cliente relacionado
               .AsQueryable();

            return query.FirstOrDefault(x => x.Email == email);
        }

        public List<string> ObterPermissoesPorPerfil(int perfilId)
        {
            var permissoes =
                (from opPerfil in _context.OperacaoPerfil
                 join operacao in _context.Operacao on opPerfil.OperacaoId equals operacao.Id
                 where opPerfil.PerfilId == perfilId
                 select operacao.Descricao)
                .Distinct()
                .ToList();

            return permissoes;
        }
    }
}
