using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Interfaces.Services;
using AgendaApp.Core.Domain.Service;
using AgendaApp.Data.Entities;

namespace AgendaApp.API.Services
{
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        public CategoriaService(ICategoriaRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
