using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IBaseService <TEntity> : IDisposable 
        where TEntity : Base
    {
        TEntity Inserir(TEntity entity);
        TEntity Atualizar(int id, TEntity entity);
        void Excluir(int id);
        TEntity ObterPorId(int id);
        List<TEntity> Listar(BaseFiltro filtro);

    }
}
