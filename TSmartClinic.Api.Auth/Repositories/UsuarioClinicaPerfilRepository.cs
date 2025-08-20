using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.Api.Auth.Repositories
{
    public class UsuarioClinicaPerfilRepository : IUsuarioClientePerfilRepository
    {


        private readonly TSmartClinicContext _tsmartClinicContext;

        public UsuarioClinicaPerfilRepository(TSmartClinicContext tsmartClinicContext) 
        {
            _tsmartClinicContext = tsmartClinicContext;
        }

        public Cliente ObterClinicaPadraoDoUsuario(int usuarioId)
        {
            return _tsmartClinicContext.UsuarioClientePerfil
                    .Where(uc => uc.UsuarioId == usuarioId && uc.ClientePadrao)
                    .Select(uc => uc.Cliente)   // seleciona a entidade Cliente
                    .FirstOrDefault();          // retorna o primeiro ou null se não existir
        }

        public List<Cliente> ObterClinicasDoUsuario(int usuarioId)
        {
            return _tsmartClinicContext.UsuarioClientePerfil
                 .Where(uc => uc.UsuarioId == usuarioId)
                 .Select(uc => uc.Cliente)
                 .ToList();
        }
     
    }
}

