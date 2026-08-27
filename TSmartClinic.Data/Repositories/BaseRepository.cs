using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using Npgsql; // PostgresException, PostgresErrorCodes
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static TSmartClinic.Core.Domain.Exceptions.GravacaoChaveInexistenteException;
using TSmartClinic.Core.Domain.Interfaces.Entities;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : Base
    {
        protected readonly DbContext? _dbContext;
        protected readonly DbSet<TEntity>? _dbSet;
        protected readonly IUsuarioLogadoService? _usuarioLogadoService;

        // No BaseRepository
        public BaseRepository(TSmartClinicContext dbContext, IUsuarioLogadoService? usuarioLogadoService = null)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _usuarioLogadoService = usuarioLogadoService;
        } 
    
        public virtual TEntity Atualizar(TEntity entity)
        {
            try
            {
                AplicarClienteNaEntidade(entity);

                _dbSet?.Update(entity);
                _dbContext?.SaveChanges();

                return entity;
            }

            // PostgreSQL
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pg)
                {
                    switch (pg.SqlState)
                    {
                        case PostgresErrorCodes.ForeignKeyViolation:    // 23503 - FK inválida
                            throw new GravacaoChaveInexistenteException();

                        case PostgresErrorCodes.UniqueViolation:        // 23505 - UNIQUE duplicada
                            throw new RegistroDuplicadoException();

                        case PostgresErrorCodes.NotNullViolation:       // 23502 - Campo obrigatório nulo
                            throw new CampoObrigatorioException();

                        case PostgresErrorCodes.CheckViolation:         // 23514 - Violou regra CHECK
                            throw new ExclusaoRegistroAssociadoException();
                    }
                }



                // (Opcional) Se ainda houver caminhos usando SQL Server, mantenha este bloco:
                // if (ex.InnerException is SqlException sqlEx)
                // {
                //     if (sqlEx.Number == 547)   // FK
                //         throw new GravacaoChaveInexistenteException();
                //     if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // unique
                //         throw new RegistroDuplicadoException();
                // }

                throw; // preserva stack trace para outros erros
            }

            catch (PostgresException pg) // caso alguma operação lance direto (sem DbUpdateException)
            {
                if (pg.SqlState == PostgresErrorCodes.ForeignKeyViolation)
                    throw new GravacaoChaveInexistenteException();
                if (pg.SqlState == PostgresErrorCodes.UniqueViolation)
                    throw new RegistroDuplicadoException();
                if (pg.SqlState == PostgresErrorCodes.NotNullViolation)
                    throw new CampoObrigatorioException();

                throw;
            }
        }


        public virtual void Excluir(TEntity entity)
        {
            try
            {
                _dbSet?.Remove(entity);
                _dbContext?.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // PostgreSQL
                if (ex.InnerException is PostgresException pg)
                {
                    switch (pg.SqlState)
                    {
                        case PostgresErrorCodes.ForeignKeyViolation:    // 23503
                            throw new GravacaoChaveInexistenteException();

                        case PostgresErrorCodes.UniqueViolation:        // 23505
                            throw new RegistroDuplicadoException();     // se você tiver essa exception

                        case PostgresErrorCodes.NotNullViolation:       // 23502
                            throw new CampoObrigatorioException();      // se você tiver essa exception
                    }
                }

                // (Opcional) Se ainda houver caminhos usando SQL Server, mantenha este bloco:
                // if (ex.InnerException is SqlException sqlEx)
                // {
                //     if (sqlEx.Number == 547)   // FK
                //         throw new GravacaoChaveInexistenteException();
                //     if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // unique
                //         throw new RegistroDuplicadoException();
                // }

                throw; // preserva stack trace original
            }
            catch (PostgresException pg) // caso alguma operação lance direto (sem DbUpdateException)
            {
                if (pg.SqlState == PostgresErrorCodes.ForeignKeyViolation)
                    throw new GravacaoChaveInexistenteException();
                if (pg.SqlState == PostgresErrorCodes.UniqueViolation)
                    throw new RegistroDuplicadoException();
                if (pg.SqlState == PostgresErrorCodes.NotNullViolation)
                    throw new CampoObrigatorioException();

                throw;
            }
        }

        public virtual void ExcluirPorChaves(params object[] keyValues)
        {
            try
            {
                if (keyValues == null || keyValues.Length == 0)
                    throw new ArgumentException("É necessário informar ao menos um valor de chave.", nameof(keyValues));

                // Tenta encontrar a entidade no contexto/BD (funciona com chave composta na ordem das PKs)
                var entity = _dbSet.Find(keyValues);

                if (entity == null)
                {
                    // Se não achou, cria um stub da entidade e popula as propriedades da chave primária
                    // para poder anexar e marcar como Deleted (gera DELETE sem carregar toda a entidade).
                    entity = Activator.CreateInstance<TEntity>();

                    var keyProperties = _dbContext.Model
                        .FindEntityType(typeof(TEntity))
                        .FindPrimaryKey()
                        .Properties
                        .ToList();

                    if (keyProperties.Count != keyValues.Length)
                        throw new InvalidOperationException("Número de valores de chave diferente do número de propriedades PK da entidade.");

                    for (int i = 0; i < keyProperties.Count; i++)
                    {
                        var propName = keyProperties[i].Name;
                        var propInfo = typeof(TEntity).GetProperty(propName);
                        if (propInfo == null)
                            throw new InvalidOperationException($"Propriedade PK '{propName}' não encontrada em {typeof(TEntity).Name}.");

                        var targetValue = Convert.ChangeType(keyValues[i], Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType);
                        propInfo.SetValue(entity, targetValue);
                    }

                    // Anexa ao context e marca para remoção
                    _dbSet.Attach(entity);
                }

                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // PostgreSQL
                if (ex.InnerException is PostgresException pg)
                {
                    switch (pg.SqlState)
                    {
                        case PostgresErrorCodes.ForeignKeyViolation:    // 23503
                            throw new GravacaoChaveInexistenteException();

                        case PostgresErrorCodes.UniqueViolation:        // 23505
                            throw new RegistroDuplicadoException();     // se você tiver essa exception

                        case PostgresErrorCodes.NotNullViolation:       // 23502
                            throw new CampoObrigatorioException();      // se você tiver essa exception
                    }
                }

                // (Opcional) Se ainda houver caminhos usando SQL Server, mantenha este bloco:
                // if (ex.InnerException is SqlException sqlEx)
                // {
                //     if (sqlEx.Number == 547)   // FK
                //         throw new GravacaoChaveInexistenteException();
                //     if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // unique
                //         throw new RegistroDuplicadoException();
                // }

                throw; // preserva stack trace original
            }
            catch (PostgresException pg) // caso alguma operação lance direto (sem DbUpdateException)
            {
                if (pg.SqlState == PostgresErrorCodes.ForeignKeyViolation)
                    throw new GravacaoChaveInexistenteException();
                if (pg.SqlState == PostgresErrorCodes.UniqueViolation)
                    throw new RegistroDuplicadoException();
                if (pg.SqlState == PostgresErrorCodes.NotNullViolation)
                    throw new CampoObrigatorioException();

                throw;
            }
        }


        public virtual TEntity Inserir(TEntity entity)
        {
            try
            {
                AplicarClienteNaEntidade(entity);

                _dbSet?.Add(entity);
                _dbContext?.SaveChanges();

                return entity;
            }
            catch (DbUpdateException ex)
            {
                // PostgreSQL
                if (ex.InnerException is PostgresException pg)
                {
                    switch (pg.SqlState)
                    {
                        case PostgresErrorCodes.ForeignKeyViolation:    // 23503
                            throw new GravacaoChaveInexistenteException();

                        case PostgresErrorCodes.UniqueViolation:        // 23505
                            throw new RegistroDuplicadoException();     // se você tiver essa exception

                        case PostgresErrorCodes.NotNullViolation:       // 23502
                            throw new CampoObrigatorioException();      // se você tiver essa exception
                    }
                }

                // (Opcional) Se ainda houver caminhos usando SQL Server, mantenha este bloco:
                // if (ex.InnerException is SqlException sqlEx)
                // {
                //     if (sqlEx.Number == 547)   // FK
                //         throw new GravacaoChaveInexistenteException();
                //     if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // unique
                //         throw new RegistroDuplicadoException();
                // }

                throw; // preserva stack trace original
            }
            catch (PostgresException pg) // caso alguma operação lance direto (sem DbUpdateException)
            {
                if (pg.SqlState == PostgresErrorCodes.ForeignKeyViolation)
                    throw new GravacaoChaveInexistenteException();
                if (pg.SqlState == PostgresErrorCodes.UniqueViolation)
                    throw new RegistroDuplicadoException();
                if (pg.SqlState == PostgresErrorCodes.NotNullViolation)
                    throw new CampoObrigatorioException();

                throw;
            }
        }

        public virtual List<TEntity> Listar(BaseFiltro filtro, params Expression<Func<TEntity, object>>[] properties)
        {
            var query = MontarFiltro(filtro, properties);

            query = AplicarFiltroCliente(query);

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
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

            // Aplica filtro automático por clínica
            query = AplicarFiltroCliente(query);

            return query?.FirstOrDefault(x => x.Id == id);

        }

        public virtual TEntity ObterPorPublicId(Guid publicId, params Expression<Func<TEntity, object>>[] properties)
        {
            if (!typeof(IEntidadeComPublicId).IsAssignableFrom(typeof(TEntity)))
            {
                throw new InvalidOperationException($"A entidade {typeof(TEntity).Name} não implementa IEntidadeComPublicId.");
            }

            IQueryable<TEntity> query = _dbSet.AsQueryable();

            // Aplica Includes recebidos
            if (properties != null)
            {
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }

            // Mantém a proteção por clínica
            query = AplicarFiltroCliente(query);

            // EF.Property permite o EF traduzir PublicId para SQL
            return query.FirstOrDefault(x => EF.Property<Guid>(x, "PublicId") == publicId);
        }

        //aplicar filto por CLienteId(Clinica)
        protected IQueryable<TEntity> AplicarFiltroCliente(IQueryable<TEntity> query)
        {
            if (!typeof(IEntidadePorCliente).IsAssignableFrom(typeof(TEntity)))
                return query;

            var clienteId = _usuarioLogadoService?.ClienteId;

            if (!clienteId.HasValue || clienteId.Value <= 0)
                return query;

            return query.Where(x => EF.Property<int>(x, "ClienteId") == clienteId.Value);
        }

        protected void AplicarClienteNaEntidade(TEntity entity)
        {
            if (entity is not IEntidadePorCliente entidadePorCliente)
                return;

            var clienteId = _usuarioLogadoService?.ClienteId;

            if (!clienteId.HasValue || clienteId.Value <= 0)
                throw new UnauthorizedAccessException("Clínica do usuário não identificada.");

            entidadePorCliente.ClienteId = clienteId.Value;
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
