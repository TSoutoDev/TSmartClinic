namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IPermissaoCacheService
    {
        List<string>? ObterPermissoes(int usuarioId, int unidadeId);
        void SalvarPermissoes(int usuarioId, int unidadeId, IEnumerable<string> permissoes);
        void RemoverPermissoes(int usuarioId, int unidadeId);
    }
}
