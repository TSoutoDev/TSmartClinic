using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Helpers.FilterHelper;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(TSmartClinicContext context, IUsuarioLogadoService usuarioLogadoService) : base(context, usuarioLogadoService)
        {
        }

        public override Paciente ObterPorId(int id, params Expression<Func<Paciente, object>>[] properties)
        {
            var query = _dbSet
                .Include(x => x.Convenio)
                .Include(x => x.PacienteEnderecos)
                    .ThenInclude(x => x.Endereco)
                .AsQueryable();

            query = AplicarFiltroCliente(query);

            return query.FirstOrDefault(x => x.Id == id);
        }

        public override Paciente ObterPorPublicId(Guid publicId, params Expression<Func<Paciente, object>>[] properties)
        {
            var query = _dbSet
                .Include(x => x.Convenio)
                .Include(x => x.PacienteEnderecos)
                    .ThenInclude(x => x.Endereco)
                .AsQueryable();

            query = AplicarFiltroCliente(query);

            return query.FirstOrDefault(x => x.PublicId == publicId);
        }

        public override List<Paciente> Listar( BaseFiltro filtro, params Expression<Func<Paciente, object>>[] properties)
        {
            var query = MontarFiltro(filtro, properties);

            query = AplicarFiltroCliente(query);

            query = query
                .Include(x => x.Convenio)
                .Include(x => x.PacienteEnderecos)
                    .ThenInclude(x => x.Endereco);

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                var nome = filtro.Nome.Trim();
                query = query.Where(x => x.NomePaciente != null && EF.Functions.ILike(x.NomePaciente, $"%{nome}%"));
            }

            if (filtro.PaginaAtual > 0 && filtro.ItensPorPagina > 0)
            {
                var pagina = filtro.PaginaAtual - 1;

                query = query
                    .Skip(pagina * filtro.ItensPorPagina)
                    .Take(filtro.ItensPorPagina);
            }

            return query.ToList();
        }

        public void ExcluirComEnderecos(Paciente paciente)
        {
            var enderecos = paciente.PacienteEnderecos?
                .Where(x => x.Endereco != null)
                .Select(x => x.Endereco!)
                .ToList();

            _dbSet?.Remove(paciente);

            if (enderecos != null && enderecos.Any())
            {
                _dbContext?.Set<Endereco>().RemoveRange(enderecos);
            }

            _dbContext?.SaveChanges();
        }
    }
}