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
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly ICriptografiaProvider _criptografiaProvider;

        public UsuarioRepository(
            IUsuarioLogadoService usuarioLogadoService,
            IMapper mapper,
            TSmartClinicContext tSmartClinicContext,
            ICriptografiaProvider criptografiaProvider = null
        ) : base(tSmartClinicContext, usuarioLogadoService)
        {
            _mapper = mapper;
            _dbContext = tSmartClinicContext;
            _usuarioLogadoService = usuarioLogadoService;
            _criptografiaProvider = criptografiaProvider;
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
             .Include(x => x.Cliente)
             .Include(x => x.UsuarioUnidadePerfil)
                 .ThenInclude(x => x.Perfil)
             .Include(x => x.UsuarioUnidadePerfil)
                 .ThenInclude(x => x.Unidade);

            var usuario = query?.FirstOrDefault();

            return usuario;

        }

        public override List<Usuario> Listar(BaseFiltro filtro, params Expression<Func<Usuario, object>>[] properties)
        {

            var filtroUsuario = filtro as BaseFiltro;

            var query = MontarFiltro(filtro, properties);

            query = query
            .Include(x => x.Cliente)
            .Include(x => x.UsuarioUnidadePerfil)
                .ThenInclude(x => x.Perfil)
            .Include(x => x.UsuarioUnidadePerfil)
                .ThenInclude(x => x.Unidade);

            // Não mostrar usuário master (exceto se o próprio estiver logado)
            if (!_usuarioLogadoService.UsuarioMaster)
            {
                query = query.Where(u => u.TipoUsuario != 'M');
            }

            //Filtrar pelo nome se estiver presente no filtro
            if (!string.IsNullOrWhiteSpace(filtroUsuario.Nome))
            {
                var nome = filtroUsuario.Nome.Trim().ToUpper();
                query = query.Where(c => EF.Functions.ILike(c.Nome, $"%{filtro.Nome.Trim()}%"));
               // query = query.Where(c => c.Nome.ToUpper().Contains(filtroUsuario.Nome));

            }


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
                .FirstOrDefault(x => x.Id == entity.Id);

            if (usuarioDb == null)
                throw new Exception("Usuário não encontrado.");

            usuarioDb.Atualizar(entity);

            _dbContext.SaveChanges();

            return usuarioDb;
        }

        public override void Excluir(Usuario entity)
        {
            _dbContext.Entry(entity)
                .Collection(u => u.UsuarioUnidadePerfil)
                .Load();

            if (entity.UsuarioUnidadePerfil != null && entity.UsuarioUnidadePerfil.Any())
            {
                _dbContext.UsuarioUnidadePerfil.RemoveRange(entity.UsuarioUnidadePerfil);
            }

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

        public override Usuario ObterPorPublicId(Guid publicId, params Expression<Func<Usuario, object>>[] properties)
        {
            var query = _dbSet
             .Include(u => u.Cliente)
             .Include(u => u.UsuarioUnidadePerfil)
                 .ThenInclude(uup => uup.Perfil)
             .Include(u => u.UsuarioUnidadePerfil)
                 .ThenInclude(uup => uup.Unidade)
             .AsQueryable();

            query = AplicarFiltroCliente(query);

            return query.FirstOrDefault(u => u.PublicId == publicId);
        }

        public List<string> ObterPermissoesPorPerfil(int perfilId)
        {
            var permissoes =
                (from opPerfil in _dbContext.OperacaoPerfil
                 join operacao in _dbContext.Operacao
                     on opPerfil.OperacaoId equals operacao.Id
                 where opPerfil.PerfilId == perfilId
                 select operacao.Descricao)
                .Distinct()
                .ToList();

            return permissoes;
        }
        public void DefinirUnidadePadrao(int usuarioId, int unidadeId)
        {
            var vinculos = _dbContext.UsuarioUnidadePerfil.Where(x => x.UsuarioId == usuarioId).ToList();

            if (!vinculos.Any(x => x.UnidadeId == unidadeId))
                throw new InvalidOperationException("Usuário não possui acesso à unidade informada.");

            foreach (var vinculo in vinculos)
                vinculo.UnidadePadrao = vinculo.UnidadeId == unidadeId;

            _dbContext.SaveChanges();
        }
    }
}
