using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class UsuarioClientePerfilRepository :  IUsuarioClientePerfilRepository
    {
        private readonly TSmartClinicContext _dbContext;
        public UsuarioClientePerfilRepository(TSmartClinicContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void RemoverPorUsuarioId(int usuarioId)
        {
            var existentes = _dbContext.UsuarioClientePerfil
                .Where(x => x.UsuarioId == usuarioId)
                .ToList();

            _dbContext.UsuarioClientePerfil.RemoveRange(existentes);
        }

        public void AdicionarRange(IEnumerable<UsuarioClientePerfil> lista)
        {
            _dbContext.UsuarioClientePerfil.AddRange(lista);
        }

        public Cliente ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
