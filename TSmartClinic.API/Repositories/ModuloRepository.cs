using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class ModuloRepository : BaseRepository<Modulo>, IModuloRepository
    {
        public ModuloRepository(TSmartClinicContext dbContext) : base(dbContext)
        {
        }
        public Task<List<Modulo>> ListarPermissoesAsync(CancellationToken ct = default)
        {
            return _dbSet
                .AsNoTracking()
                .AsSplitQuery() // opcional, ajuda a evitar cartesian explosion
                .Include(m => m.Funcionalidades)
                    .ThenInclude(f => f.Operacoes)
                .OrderBy(m => m.NomeModulo)
                .ToListAsync(ct);
        }


        public async Task<List<Modulo>> ListarModulos()
        {
            return await _dbSet
                .OrderBy(x => x.NomeModulo)
                .ToListAsync();
        }

        public async Task<List<Modulo>> ListarIdsPorPerfilAsync(int idPerfil, CancellationToken ct = default)
        {
            var result = await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .Where(m => m.Funcionalidades
                    .Any(f => f.Operacoes
                        .Any(o => o.OperacaoPerfis
                            .Any(op => op.Id == idPerfil))))
                .Select(m => new Modulo
                {
                    Id = m.Id,
                    NomeModulo = m.NomeModulo,
                    Descricao = m.Descricao,
                    Ativo = m.Ativo,
                    Funcionalidades = m.Funcionalidades
                        .Select(f => new Funcionalidade
                        {
                            Id = f.Id,
                            NomeFuncionalidade = f.NomeFuncionalidade,
                            Descricao = f.Descricao,
                            ModuloId = f.ModuloId,
                            Operacoes = f.Operacoes
                                .Where(o => o.OperacaoPerfis.Any(op => op.Id == idPerfil))
                                .ToList()
                        })
                        .Where(f => f.Operacoes.Any()) // só traz funcionalidades com operações do perfil
                        .ToList()
                })
                .OrderBy(m => m.NomeModulo)
                .ToListAsync(ct);

            return result;
        }

    }
}
