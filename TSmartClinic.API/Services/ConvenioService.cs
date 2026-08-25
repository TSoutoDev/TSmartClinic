using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class ConvenioService : BaseService<Convenio>, IConvenioService
    {
        public ConvenioService(IConvenioRepository convenioRepository) : base(convenioRepository)
        {
        }
    }
}
