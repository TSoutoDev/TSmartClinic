using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class PerfilService : BaseService<Perfil>, IPerfilService
    {
        public PerfilService(IPerfilRepository perfilRepository) : base(perfilRepository)
        {
        }

        public override Perfil Inserir(Perfil entity)
        {

            return base.Inserir(entity);
        }
    }
}
