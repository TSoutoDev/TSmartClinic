using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class OperacaoService : BaseService<Operacao>, IOperacaoService
    {
        private readonly IOperacaoRepository _operacaoRepository;
        public OperacaoService(IOperacaoRepository operacaoRepository) : base(operacaoRepository)
        {
            _operacaoRepository = operacaoRepository;
        }

        public async Task<List<Operacao>> ListarOperacoes()
        {
            return await _operacaoRepository.ListarOperacoes();
        }
    }
}
