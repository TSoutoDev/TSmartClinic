using AgendaApp.Core.Domain.Entities;
using AgendaApp.Core.Domain.Exceptions;
using AgendaApp.Core.Domain.Helpers;
using AgendaApp.Core.Domain.Helpers.FilterHelper;
using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Interfaces.Services;

namespace AgendaApp.Core.Domain.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : Base
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual TEntity Atualizar(int id, TEntity entity)
        {
            //Recupera o registro no banco
            TEntity entityBanco = _baseRepository?.ObterPorId(id);

            //Se não encontrou estoura exceção
            if (entityBanco == null) throw new NotFoundException();

            //Atualiza o registro do banco com os valores que vieram da API (ou frontend)
            entityBanco.Atualizar(entity);

            //Atualiza o registro NO banco
            _baseRepository?.Atualizar(entityBanco);

            return entity;
        }

        public virtual void Excluir(int id)
        {
            //Recupera o registro no banco
            TEntity entityBanco = _baseRepository?.ObterPorId(id);

            //Se não encontrou estoura exceção
            if (entityBanco == null) throw new NotFoundException();

            _baseRepository.Excluir(entityBanco);
        }

        public virtual TEntity Inserir(TEntity entity)
        {
            //limpa os campos texto
            entity.RemoverEspacosEmBranco();

            return _baseRepository?.Inserir(entity);
        }

        public virtual List<TEntity> Listar(BaseFiltro filtro)
        {
            return _baseRepository.Listar(filtro);
        }

        public virtual TEntity ObterPorId(int id)
        {
            return _baseRepository?.ObterPorId(id);
        }
        public void Dispose()
        {
            _baseRepository?.Dispose();
        }
    }
}
