using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.XPath;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Infra.Security.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {

        private readonly IMapper _mapper;
        private readonly TSmartClinicContext _dbContext;
        private readonly IUsuarioClientePerfilRepository _operacaoPerfilRepository;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly ICriptografiaProvider _criptografiaProvider;

        public UsuarioRepository(
            IUsuarioLogadoService usuarioLogadoService,
            IUsuarioClientePerfilRepository usuarioClientePerfilRepository,
            IMapper mapper,
            TSmartClinicContext tSmartClinicContext,
            ICriptografiaProvider criptografiaProvider = null
        ) : base(tSmartClinicContext)
        {
            _mapper = mapper;
            _dbContext = tSmartClinicContext;
            _operacaoPerfilRepository = usuarioClientePerfilRepository;
            _usuarioLogadoService = usuarioLogadoService;
            _criptografiaProvider = criptografiaProvider;
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clientesUsuario)
        {

            throw new NotImplementedException();
        }

        public Usuario ObterPorEmail(string email)
        {
            var query = _dbSet as IQueryable<Usuario>;

            return query?.FirstOrDefault(x => x.Email == email);
        }

        public override Usuario ObterPorId(int id, params Expression<Func<Usuario, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Usuario>;

            query = query?.Where(x => (int)x.Id == id);

            query = query?
                .Include(x => x.UsuarioClientePerfil)
                .ThenInclude(x => x.Perfil)
                .ThenInclude(x => x.Cliente);

            var usuario = query?.FirstOrDefault();

            return usuario;

        }

        public override List<Usuario> Listar(BaseFiltro filtro, params Expression<Func<Usuario, object>>[] properties)
        {
            var query = _dbSet as IQueryable<Usuario>;

            var filtroPerfil = filtro as BaseFiltro;

            query = MontarFiltro(filtro, properties);

            query = query
                .Include(x => x.Cliente)
                .Include(x => x.UsuarioClientePerfil)
                    .ThenInclude(x => x.Perfil);

            // Não mostrar usuário master (exceto se o próprio estiver logado)
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                query = query.Where(u => u.TipoUsuario != 'M');
            }

            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroPerfil.Nome))
                query = query.Where(c => c.Nome.ToUpper().Contains(filtroPerfil.Nome));

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;
                query = query.Skip(pagina * filtro.ItensPorPagina)
                             .Take(filtro.ItensPorPagina);
            }

            return query.ToList();

        }

        public override Usuario Atualizar(Usuario entity)
        {
            var usuarioDb = _dbSet
                .Include(u => u.UsuarioClientePerfil) // importante: carregar a coleção
                .FirstOrDefault(p => p.Id == entity.Id);

            if (usuarioDb == null) throw new Exception("Usuário não encontrado");

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                using var transaction = _dbContext.Database.BeginTransaction();
                try
                {
                    // 1) Atualiza campos simples
                    _mapper.Map(entity, usuarioDb);

                    // 2) Delta de UsuarioClientePerfil
                    var atuais = usuarioDb.UsuarioClientePerfil.ToList();

                    var novos = entity.UsuarioClientePerfil?
                        .Select(x => new UsuarioClientePerfil
                        {
                            UsuarioId = usuarioDb.Id,
                            ClienteId = x.ClienteId,
                            PerfilId = x.PerfilId,
                            ClientePadrao = x.ClientePadrao
                        }).ToList() ?? new List<UsuarioClientePerfil>();

                    // 2a) Remover vínculos que não existem mais
                    var paraRemover = atuais
                        .Where(atual => !novos.Any(n =>
                            n.ClienteId == atual.ClienteId &&
                            n.PerfilId == atual.PerfilId))
                        .ToList();

                    if (paraRemover.Any())
                        _dbContext.RemoveRange(paraRemover);

                    // 2b) Adicionar vínculos que não existiam
                    var paraAdicionar = novos
                        .Where(novo => !atuais.Any(a =>
                            a.ClienteId == novo.ClienteId &&
                            a.PerfilId == novo.PerfilId))
                        .ToList();

                    if (paraAdicionar.Any())
                        _dbContext.AddRange(paraAdicionar);

                    // 2c) Atualizar vínculos que continuam (ex.: ClientePadrao pode mudar)
                    foreach (var atual in atuais)
                    {
                        var correspondente = novos.FirstOrDefault(n =>
                            n.ClienteId == atual.ClienteId &&
                            n.PerfilId == atual.PerfilId);

                        if (correspondente != null)
                        {
                            atual.ClientePadrao = correspondente.ClientePadrao;
                            // se houver outros campos além de ClientePadrao, atualize aqui
                        }
                    }

                    // 3) Persiste tudo
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            });

            // Recarrega navegação
            _dbContext.Entry(usuarioDb).Collection(p => p.UsuarioClientePerfil).Load();

            return usuarioDb;
        }

        public override void Excluir(Usuario entity)
        {
            // Carregar vínculos
            _dbContext.Entry(entity)
                .Collection(u => u.UsuarioClientePerfil)
                .Load();

            // Remover vínculos primeiro
            if (entity.UsuarioClientePerfil.Any())
            {
                _dbContext.UsuarioClientePerfil.RemoveRange(entity.UsuarioClientePerfil);
            }

            // Agora remover o usuário
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();

        }

        public void AtualizarSenhaHash(int usuarioId, string senhaHash)
        {
            var usuario = _dbSet.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.DefinirSenhaPrimeiroAcesso(senhaHash);
            usuario.PrimeiroAcesso = false;

            _dbContext.SaveChanges();
        }
    }
}
