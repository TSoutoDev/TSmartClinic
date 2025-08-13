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
        public UsuarioRepository(TSmartClinicContext? TSmartClinicContext) : base(TSmartClinicContext)
        {
            _context = TSmartClinicContext;
        }
        public List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clinicasUsuario)
        {
            if (clinicasUsuario == null || !clinicasUsuario.Any())
                return new List<string>();

            // 1. Obter todos os PerfilIds do usuário para essas clínicas
            var clinicaIds = clinicasUsuario.Select(c => c.Id).ToList();

            var perfilIds = _context.UsuarioClientePerfil
                .Where(ucp => ucp.Id == usuarioId && clinicaIds.Contains(ucp.ClienteId))
                .Select(ucp => ucp.PerfilId)
                .Distinct()
                .ToList();

            if (!perfilIds.Any())
                return new List<string>();

            // 2. Buscar permissões (operações) desses perfis
            var permissoes = (from opPerfil in _context.OperacaoPerfil
                              join operacao in _context.Operacao on opPerfil.OperacaoId equals operacao.Id
                              join funcionalidade in _context.Funcionalidade on operacao.FuncionalidadeId equals funcionalidade.Id
                              where perfilIds.Contains(opPerfil.Id)
                              select operacao.Descricao)
                             .Distinct()
                             .ToList();

            return permissoes;
        }
        //public List<string> ObterPermissaoUsuario(int usuarioId, int clinicaId )
        //{
        //    // 1. Buscar o perfil do usuário para a clínica
        //    var perfilId = _context.UsuarioClinicaPerfil
        //        .Where(u => u.UsuarioId == usuarioId && u.ClinicaId == clinicaId)
        //        .Select(u => u.PerfilId)
        //        .FirstOrDefault();

        //    if (perfilId == 0)
        //        return new List<string>();


        //    // 2. Buscar permissões associadas a esse perfil
        //    var query = from opPerfil in _context.OperacaoPerfil
        //                join operacao in _context.Operacao on opPerfil.OperacaoId equals operacao.Id
        //                join funcionalidade in _context.Funcionalidade on operacao.FuncionalidadeId equals funcionalidade.Id
        //                where opPerfil.PerfilId == perfilId
        //                select new { operacao.Descricao, funcionalidade.ModuloId };

        //    // 3. Filtrar por módulo se necessário
        //    //if (moduloId != null)
        //    //{
        //    //    query = query.Where(p => p.ModuloId == moduloId);
        //    //}

        //    return query.Select(p => p.Descricao)
        //        .Distinct()
        //        .ToList();
        //}



        public Usuario ObterPorEmail(string email)
        {
            var query = _dbSet
               .Include(u => u.Cliente)  // Inclui o Cliente relacionado
               .AsQueryable();

            return query.FirstOrDefault(x => x.Email == email);
        }
    }
}
