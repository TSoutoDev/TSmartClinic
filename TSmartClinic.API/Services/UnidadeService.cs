using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;

namespace TSmartClinic.API.Services
{
    public class UnidadeService : BaseService<Unidade>, IUnidadeService
    {
        private readonly IUnidadeRepository _unidadeRepository;

        public UnidadeService(IUnidadeRepository unidadeRepository) : base(unidadeRepository)
        {
            _unidadeRepository = unidadeRepository;
        }

        public List<Unidade> ListarPorCliente(int clienteId)
        {
            return _unidadeRepository.ListarPorCliente(clienteId);
        }
    }
}