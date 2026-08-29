using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Services
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository _municipioRepository;

        public MunicipioService(IMunicipioRepository municipioRepository)
        {
            _municipioRepository = municipioRepository;
        }

        public async Task<Municipio?> ObterPorId(int id)
        {
            return await _municipioRepository.ObterPorId(id);
        }
    }
}