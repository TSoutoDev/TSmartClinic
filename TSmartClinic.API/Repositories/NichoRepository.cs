using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class NichoRepository : BaseRepository<Nicho>
    {
        public NichoRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }
    }
}
