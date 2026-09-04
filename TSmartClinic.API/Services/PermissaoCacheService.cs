using Microsoft.Extensions.Caching.Memory;
using TSmartClinic.Core.Domain.Interfaces.Services;

namespace TSmartClinic.API.Services
{
    public class PermissaoCacheService : IPermissaoCacheService
    {
        private readonly IMemoryCache _cache;

        public PermissaoCacheService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private static string ObterChave(int usuarioId, int unidadeId)
        {
            return $"permissoes_usuario_{usuarioId}_{unidadeId}";
        }

        public List<string>? ObterPermissoes(int usuarioId, int unidadeId)
        {
            var chave = ObterChave(usuarioId, unidadeId);

            return _cache.Get<List<string>>(chave);
        }

        public void SalvarPermissoes(int usuarioId, int unidadeId, IEnumerable<string> permissoes)
        {
            var chave = ObterChave(usuarioId, unidadeId);

            var lista = permissoes
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            _cache.Set(chave, lista, TimeSpan.FromHours(8));
        }

        public void RemoverPermissoes(int usuarioId, int unidadeId)
        {
            var chave = ObterChave(usuarioId, unidadeId);

            _cache.Remove(chave);
        }
    }
}