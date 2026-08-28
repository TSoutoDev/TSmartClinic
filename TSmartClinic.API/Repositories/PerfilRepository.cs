using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        private readonly TSmartClinicContext _dbContext;
        private readonly IOperacaoPerfilRepository _operacaoPerfilRepository;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        public PerfilRepository(IUsuarioLogadoService usuarioLogadoService, IOperacaoPerfilRepository operacaoPerfilRepository, TSmartClinicContext dbContext, TSmartClinicContext context) : base(dbContext, usuarioLogadoService)
        {
            _dbContext = context;
            _operacaoPerfilRepository = operacaoPerfilRepository;
            _usuarioLogadoService = usuarioLogadoService;
        }
        public override Perfil ObterPorPublicId(Guid publicId, params Expression<Func<Perfil, object>>[] properties)
        {
            var query = _dbSet
                .Include(x => x.Nicho)
                .Include(x => x.Cliente)
                .Include(x => x.OperacaoPerfis)
                .AsQueryable();

            query = AplicarFiltroCliente(query);

            return query.FirstOrDefault(x => x.PublicId == publicId);
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
           // var query = _dbSet as IQueryable<Perfil>;

            var filtroPerfil = filtro as BaseFiltro;

            var query = MontarFiltro(filtro, properties);

            query = query
                .Include(x => x.Nicho)
                .Include(x => x.Cliente);

            // Não mostrar perfil master para usuários que não são master
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                query = query.Where(u => !u.UsuarioClientePerfil.Any(p => p.PerfilId == 1));
            }

            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroPerfil?.Nome))
            {
                var nome = filtroPerfil.Nome.Trim().ToUpper();
                //query = query.Where(c => c.NomePerfil.ToUpper().Contains(filtroPerfil.Nome));
                query = query.Where(c => EF.Functions.ILike(c.NomePerfil, $"%{filtroPerfil.Nome.Trim()}%"));
            }


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
            var perfilDb = _dbSet.Include(p => p.OperacaoPerfis).FirstOrDefault(p => p.Id == entity.Id);

            if (perfilDb == null)
                throw new Exception("Perfil não encontrado");

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                using var transaction = _dbContext.Database.BeginTransaction();

                try
                {
                    // Campos do Perfil
                    perfilDb.NomePerfil = entity.NomePerfil;
                    perfilDb.ValidadeDias = entity.ValidadeDias;
                    perfilDb.ErrosSenha = entity.ErrosSenha;
                    perfilDb.ResponsavelTecnico = entity.ResponsavelTecnico;
                    perfilDb.Ativo = entity.Ativo;
                    perfilDb.NichoId = entity.NichoId;
                    perfilDb.ClienteId = entity.ClienteId;


                    // Operações recebidas da tela
                    var idsNovos = entity.OperacaoPerfis?
                        .Select(x => x.OperacaoId)
                        .Distinct()
                        .ToList()
                        ?? new List<int>();


                    // Operações atualmente existentes
                    var atuais = perfilDb.OperacaoPerfis.ToList();


                    // Remove somente as desmarcadas
                    var paraRemover = atuais
                        .Where(atual => !idsNovos.Contains(atual.OperacaoId))
                        .ToList();

                    if (paraRemover.Any())
                    {
                        _dbContext.Set<OperacaoPerfil>()
                            .RemoveRange(paraRemover);
                    }


                    // Adiciona somente as novas
                    var idsAtuais = atuais
                        .Select(x => x.OperacaoId)
                        .ToHashSet();

                    var paraAdicionar = idsNovos
                        .Where(id => !idsAtuais.Contains(id))
                        .Select(id => new OperacaoPerfil
                        {
                            PerfilId = perfilDb.Id,
                            OperacaoId = id
                        })
                        .ToList();

                    if (paraAdicionar.Any())
                    {
                        _dbContext.Set<OperacaoPerfil>()
                            .AddRange(paraAdicionar);
                    }


                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            });

            _dbContext.Entry(perfilDb)
                .Collection(p => p.OperacaoPerfis)
                .Load();

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

        public async Task<List<Perfil>> ListarPerfilPorCliente(int clienteId)
        {
            return await _dbSet
            .Include(x => x.Cliente)             // carrega navegação
            .Where(x => x.ClienteId == clienteId) // filtra pelo cliente
            .OrderBy(x => x.NomePerfil)           // ordena
            .ToListAsync();                        // retorna lista assincronamente
        }

        public async Task<List<Perfil>> ListarTodos()
        {
            var response = _dbSet      
              .OrderBy(x => x.NomePerfil)     // ordena pelo nome
              .ToList();

            return response;
        }

     
    }
}

