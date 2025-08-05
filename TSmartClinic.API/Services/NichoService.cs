using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class NichoService : BaseService<Nicho>
    {
        public NichoService(IBaseRepository<Nicho> baseRepository) : base(baseRepository)
        {
        }
    }
}
