using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Data.Entities;

namespace TSmartClinic.API.Services
{
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        public CategoriaService(ICategoriaRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
