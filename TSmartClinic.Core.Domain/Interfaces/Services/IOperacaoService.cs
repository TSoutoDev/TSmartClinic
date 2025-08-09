using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IOperacaoService : IBaseService<Operacao>
    {
        Task<List<Operacao>> ListarOperacoes();
    }
}
