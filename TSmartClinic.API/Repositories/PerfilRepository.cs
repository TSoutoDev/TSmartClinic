using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        private readonly TSmartClinicContext _dbContext;
        private readonly IOperacaoPerfilRepository _operacaoPerfilRepository;
        public PerfilRepository(IOperacaoPerfilRepository operacaoPerfilRepository, TSmartClinicContext dbContext, TSmartClinicContext context) : base(dbContext)
        {
            _dbContext = context;

            _operacaoPerfilRepository = operacaoPerfilRepository;
        }

        public override Perfil ObterPorId(int id, params Expression<Func<Perfil, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Perfil>;

            query = query?.Where(x => (int)x.Id == id);

            query = query?
                .Include(x => x.Nicho)?
                .Include(x => x.Cliente)?
                .Include(x => x.OperacaoPerfis);
                

            var perfil = query?.FirstOrDefault();

            return perfil;

        }

        public override List<Perfil> Listar(BaseFiltro filtro, params Expression<Func<Perfil, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Perfil>;

            var filtroPerfil = filtro as BaseFiltro;

            query = MontarFiltro(filtro, properties);

            query = query
                .Include(x => x.Nicho)
                .Include(x => x.Cliente);
               // .Include(x => x.OperacaoPerfis); 

            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroPerfil.Nome))
                query = query.Where(c => c.NomePerfil.ToUpper().Contains(filtroPerfil.Nome));

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;
                query = query.Skip(pagina * filtro.ItensPorPagina)
                             .Take(filtro.ItensPorPagina);
            }
            return query.ToList();
        }

        public override Perfil Atualizar(Perfil entity)
        {
            // Carrega perfil rastreado
            var perfilDb = _dbSet.FirstOrDefault(p => p.Id == entity.Id);
            if (perfilDb == null) throw new Exception("Perfil não encontrado");

            // Cria strategy para retry
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                using var transaction = _dbContext.Database.BeginTransaction();
                try
                {
                    // 1) Atualiza campos simples (apenas valores escalares)
                    perfilDb.NomePerfil = entity.NomePerfil;
                    perfilDb.Ativo = entity.Ativo;
                    // atualize outros campos escalares se houver...

                    // 2) Delta de OperacaoPerfis: implementamos como "replace" para evitar duplicação
                    // Extrai IDs vindos da UI (sem duplicatas)
                    var idsNovos = entity.OperacaoPerfis?.Select(op => op.OperacaoId).Distinct().ToList() ?? new List<int>();

                    // 2a) Remove tudo que existe para esse perfil (DELETE direto)
                    _operacaoPerfilRepository.RemoverPorPerfilId(perfilDb.Id);

                    // 2b) Cria os novos registros (somente se houver IDs)
                    if (idsNovos.Any())
                    {
                        var novos = idsNovos.Select(id => new OperacaoPerfil
                        {
                            PerfilId = perfilDb.Id,
                            OperacaoId = id
                        }).ToList();

                        // Adiciona ao ChangeTracker — _operacaoPerfilRepository.AdicionarRange() não faz SaveChanges()
                        _operacaoPerfilRepository.AdicionarRange(novos);
                    }

                    // 3) Persiste todas alterações (perfil + operacaoPerfil) de uma vez
                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            });

            // Opcional: recarregar navegações se precisar devolver operações
            _dbContext.Entry(perfilDb).Collection(p => p.OperacaoPerfis).Load();

            return perfilDb;
        }

        public override Perfil Inserir(Perfil entity)
        {
            var query = _dbSet as IQueryable<Perfil>;
            query
                .Include(x => x.Cliente)
                .Include(x => x.OperacaoPerfis);

            return base.Inserir(entity);
        }

    }
}

