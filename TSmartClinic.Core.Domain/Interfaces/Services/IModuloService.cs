using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IModuloService : IBaseService<Modulo>
    {
        Task<List<Modulo>> ListarModulos();
        /// <summary>
        /// O CancellationToken ct = default é basicamente uma forma de permitir que quem chama o seu método
        /// cancele a execução assíncrona — e = default só significa que, se ninguém passar nada, ele usa o valor padrão (CancellationToken.None, ou seja, nunca cancela).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Modulo>> ListarPermissoesAsync(CancellationToken cancellationToken = default);
        public Task<List<Modulo>> ListarIdsPorPerfilAsync(int perfilId, CancellationToken ct = default);
        Task AtualizarOperacoesDoPerfilAsync(int perfilId, IEnumerable<int> operacaoIds, CancellationToken ct = default);        // 🔹 novo: salvar/atualizar operações do perfil
    }
}
