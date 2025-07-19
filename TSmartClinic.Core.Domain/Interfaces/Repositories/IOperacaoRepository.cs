using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IOperacaoRepository : IBaseRepository<Operacao>
    {
        List<string> ObterPermissaoUsuario(int perfilId);
    }
}
