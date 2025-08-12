using Microsoft.EntityFrameworkCore;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Repositories
{
    public class OperacaoPerfilRepository :  IOperacaoPerfilRepository
    {

        //public async Task<List<int>> ListarIdsPorPerfilAsync(int perfilId, CancellationToken ct = default)
        //{
        //    return await _dbSet                     
        //     .AsNoTracking()
        //     .Where(x => x.PerfilId == perfilId)  
        //     .Select(x => x.OperacaoId)
        //     .Distinct()
        //     .ToListAsync(ct);
        //}
    }
}
