namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IUsuarioLogadoService
    {
        int? UsuarioLogadoId {  get; }
        bool UsuarioMaster { get; }
        string TipoUsuario { get; }
        int? ClienteId { get; }
    }
}
