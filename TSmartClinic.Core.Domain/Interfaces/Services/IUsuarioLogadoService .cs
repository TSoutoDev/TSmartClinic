namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioLogadoService
    {
        int? UsuarioLogadoId { get; }
        string TipoUsuario { get; }
        bool UsuarioMaster { get; }
        int? ClienteId { get; }
    }
}
