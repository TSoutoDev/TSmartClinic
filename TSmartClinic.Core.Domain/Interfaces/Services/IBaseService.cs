using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;

namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IBaseService <TEntity> : IDisposable  where TEntity : Base
    {
        TEntity Inserir(TEntity entity);
        TEntity ObterPorId(int id); // uso interno
        TEntity ObterPorPublicId(Guid publicId); // uso externo
        TEntity Atualizar(Guid publicId, TEntity entity);
        void Excluir(Guid publicId);
        List<TEntity> Listar(BaseFiltro filtro);
    }
}
