using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Services
{
    public class OperacaoPerfilService : BaseService<OperacaoPerfil>, IOperacaoPerfilService
    {
      private readonly IOperacaoPerfilRepository _baseRepository;
        public OperacaoPerfilService(IOperacaoPerfilRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        // exemplo de método útil
        public List<OperacaoPerfil> ListaOperacoPerfilPorPerfil(int perfilId)
        {
            return _baseRepository.ListaOperacoPerfilPorPerfil(perfilId);
        }
    }
}
