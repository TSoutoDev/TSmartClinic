using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Entities;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IUsuarioLogadoService usuarioLogadoService, TSmartClinicContext TSmartClinicContext) : base(TSmartClinicContext, usuarioLogadoService)
        {
        }
    }
}
