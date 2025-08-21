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
            throw new NotImplementedException();
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void AdicionarRange(IEnumerable<UsuarioClientePerfil> usuarioClientePerfis)
        {
            if (usuarioClientePerfis == null) return;
            _dbContext.Set<UsuarioClientePerfil>().AddRange(usuarioClientePerfis);
        }
    }
}
