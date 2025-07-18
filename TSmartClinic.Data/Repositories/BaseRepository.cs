using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TSmartClinic.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : Base
    {
        protected readonly DbContext? _dbContext;
        protected readonly DbSet<TEntity>? _dbSet;

        // No BaseRepository
      
        public BaseRepository(TSmartClinicContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>(); // aqui que estoura
        }


        public virtual TEntity Atualizar(TEntity entity)
        {
            try
            {
                _dbSet?.Update(entity);
                _dbContext?.SaveChanges();

                return entity;
            }
            catch (Exception e) 
            {

                if (e.InnerException is SqlException)
                    if (((SqlException)e.InnerException).Number == 547)
                        throw new GravacaoChaveInexistenteException();
                throw e;
            }
        }


        public virtual void Excluir(TEntity entity)
        {
            try
            {
                _dbSet?.Remove(entity);
                _dbContext?.SaveChanges();
            }
            catch (Exception e)
            {

                if (e.InnerException is SqlException)
                    if (((SqlException)e.InnerException).Number == 547)
                        throw new GravacaoChaveInexistenteException();
                throw e;
            }
        }

        public virtual TEntity Inserir(TEntity entity)
        {
            try
            {
                _dbSet?.Add(entity);
                _dbContext?.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {

                if (e.InnerException is SqlException)
                    if (((SqlException)e.InnerException).Number == 547)
                        throw new GravacaoChaveInexistenteException();
                throw e;
            }
        }

        public virtual List<TEntity> Listar(BaseFiltro filtro, params Expression<Func<TEntity, object>>[] properties)
        {
            var query = MontarFiltro(filtro, properties);

            if(filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;
                query = query.Skip(pagina * filtro.ItensPorPagina).Take(filtro.ItensPorPagina);
            }
            return query.ToList();
        }

        public void Dispose()
            => _dbContext?.Dispose();

        public virtual TEntity ObterPorId(int id, params Expression<Func<TEntity, object>>[] properties)
        {
            var query = _dbSet as IQueryable<TEntity>;

            //Carrega tabelas relacionadas
            query = properties.Aggregate(query, (current, property) => current?.Include(property));

            return query?.FirstOrDefault(x => x.Id == id);

        }

        protected IQueryable<TEntity> MontarFiltro(BaseFiltro filtro, params Expression<Func<TEntity, object>>[] properties)
        {
            var propsFiltro = filtro.GetType().GetProperties();
            var query = _dbSet as IQueryable<TEntity>;
            var param = Expression.Parameter(typeof(TEntity), "p");
            Expression? body = null;

            //Carrega tabelas relacionadas
            query = properties.Aggregate(query, (current, property) => current?.Include(property));

            foreach (var prop in propsFiltro)
            {
                //Recupera o valor do filtro
                object valor = prop.GetValue(filtro);

                //Se o campo de filtro tem valor
                if (valor != null)
                {
                    //Verifica se alguma propriedade do objeto tem o mesmo nome do filtro
                    var propsEntity = typeof(TEntity).GetProperties();
                    var propEntity = propsEntity.Where(x => x.Name.ToLower() == prop.Name.ToLower()).FirstOrDefault();

                    if (propEntity != null)
                    {
                        MemberExpression member = Expression.Property(param, propEntity.Name);
                        Expression constant = Expression.Constant(valor);
                        Expression expression = null;
                        Type propertyType = propEntity.PropertyType;

                        //Verifica se é um tipo genérico nullable
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = propertyType.GetGenericArguments()[0];
                            constant = Expression.Convert(constant, propEntity.PropertyType);
                        }

                        switch (propertyType.Name)
                        {
                            case nameof(String):
                                expression = Expression.Call(member, "Contains", Type.EmptyTypes, constant);
                                break;

                            case nameof(Int32):
                                expression = Expression.Equal(member, constant);
                                break;

                            case nameof(Int64):
                                expression = Expression.Equal(member, constant);
                                break;

                            case nameof(Boolean):
                                expression = Expression.Equal(member, constant);
                                break;

                            case nameof(DateTime):
                                expression = Expression.Equal(member, constant);
                                break;

                            case nameof(Char):
                                expression = Expression.Equal(member, constant);
                                break;

                            default:
                                break;
                        }

                        //Concatena as expressões
                        if (filtro.OperadorLogico.ToUpper() == "AND")
                            body = body == null ? expression : Expression.AndAlso(body, expression);
                        else
                            body = body == null ? expression : Expression.OrElse(body, expression);
                    }
                }
            }

            //Gera a expressão lambda das expressões
            if (body != null)
            {
                var clausulaWhere = Expression.Lambda<Func<TEntity, bool>>(body, param);
                query = query.Where(clausulaWhere);
            }

            return query.Take(1001);
        }

    }
}
