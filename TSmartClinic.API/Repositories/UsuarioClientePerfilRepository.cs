using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.API.Repositories
{
    public class UsuarioClientePerfilRepository :  IUsuarioClientePerfilRepository
    {
        private readonly TSmartClinicContext _dbContext;
        public UsuarioClientePerfilRepository(TSmartClinicContext dbContext) 
        {
            _dbContext = dbContext;
        }
        //public void RemoverPorUsuarioId(int usuarioId)
        //{
        //    var existentes = _dbContext.UsuarioClientePerfil
        //        .Where(x => x.UsuarioId == usuarioId)
        //        .ToList();

        //    _dbContext.UsuarioClientePerfil.RemoveRange(existentes);
        //}

        public Cliente ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            return _dbContext.UsuarioClientePerfil
                   .AsNoTracking()
                   .Where(x => x.UsuarioId == usuarioId && x.ClientePadrao)
                   .Select(x => x.Cliente)
                   .FirstOrDefault();
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            return _dbContext.UsuarioClientePerfil
                   .AsNoTracking()
                   .Where(x => x.UsuarioId == usuarioId)
                   .Select(x => x.Cliente)
                   .Distinct()
                   .OrderBy(x => x.NomeCliente)
                   .ToList();
        }

        public List<UsuarioClientePerfil> ObterListaPorUsuarioId(int usuarioId)
        {
            return _dbContext.UsuarioClientePerfil
               .Where(x => x.UsuarioId == usuarioId)
               .ToList();
        }

        public void ExluirPorUsuarioId(int usuarioId)
        {
            _dbContext.UsuarioClientePerfil
             .Where(x => x.UsuarioId == usuarioId)
             .ExecuteDelete();
        }

        public bool UsuarioPossuiAcessoClinica(int usuarioId, int clinicaId)
        {
            return _dbContext.UsuarioClientePerfil
                    .AsNoTracking()
                    .Any(x =>
                        x.UsuarioId == usuarioId &&
                        x.ClienteId == clinicaId);
        }

        public void AdicionarRange(IEnumerable<UsuarioClientePerfil> usuarioClientePerfis)
        {
            if (usuarioClientePerfis == null) return;
            _dbContext.Set<UsuarioClientePerfil>().AddRange(usuarioClientePerfis);
        }

        public void Inserir(UsuarioClientePerfil usuarioClientePerfil)
        {
            _dbContext.UsuarioClientePerfil.Add(usuarioClientePerfil);
            _dbContext.SaveChanges();
        }
    }
}
