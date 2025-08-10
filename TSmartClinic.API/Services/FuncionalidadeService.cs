using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class FuncionalidadeService : BaseService<Funcionalidade>, IFuncionalidadeService
    {

        private readonly IFuncionalidadeRepository _funcionalidadeRepository;
        public FuncionalidadeService(IFuncionalidadeRepository funcionalidadeRepository) : base(funcionalidadeRepository)
        {
            _funcionalidadeRepository = funcionalidadeRepository;
        }

        public async Task<List<Funcionalidade>> ListarFuncionalidades()
        {
            return await _funcionalidadeRepository.ListarFuncionalidades();
        }
    }
}
