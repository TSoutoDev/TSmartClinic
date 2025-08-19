using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IOperacaoPerfilRepository  : IBaseRepository<OperacaoPerfil>
    {
        public List<OperacaoPerfil> ListaOperacoPerfilPorPerfil(int id);
        void RemoverPorPerfilId(int perfilId); // Remove todas as OperacaoPerfis relacionadas a um Perfil pelo Id.
        void AdicionarRange(IEnumerable<OperacaoPerfil> operacaoPerfis);// Adiciona uma lista de OperacaoPerfis
    }
}
