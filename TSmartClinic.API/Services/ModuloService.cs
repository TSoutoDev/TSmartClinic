using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class ModuloService : BaseService<Modulo>, IModuloService
    {
        private readonly IModuloRepository _moduloRepository;
        public ModuloService(IModuloRepository moduloRepository) : base(moduloRepository)
        {
            _moduloRepository = moduloRepository;
        }

        public async Task<List<Modulo>> ListarPermissoesAsync()
        {
            return await _moduloRepository.ListarPermissoesAsync();
        }

        public async Task<List<Modulo>> ListarModulos()
        {
            return await _moduloRepository.ListarModulos();
        }
    }
}
