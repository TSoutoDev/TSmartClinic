﻿using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using System.Linq.Expressions;

namespace TSmartClinic.Core.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : Base
    {
        TEntity Inserir(TEntity entity);
        TEntity Atualizar(TEntity entity);
        void Excluir(TEntity entity);
        TEntity ObterPorId(int id, params Expression<Func<TEntity, object>>[] properties);
        List<TEntity> Listar(BaseFiltro filtro, params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// Exclui uma entidade pela(s) chave(s) primária(s).
        /// Para chave composta, passe os valores na ordem definida no modelo EF.
        /// </summary>
        void ExcluirPorChaves(params object[] keyValues);

    }
}
