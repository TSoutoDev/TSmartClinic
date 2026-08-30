namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IPermissaoCacheService
    {
        List<string>? ObterPermissoes(int usuarioId);
        void SalvarPermissoes(int usuarioId, IEnumerable<string> permissoes);
        void RemoverPermissoes(int usuarioId);
    }
}
