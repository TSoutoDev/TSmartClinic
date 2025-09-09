namespace TSmartClinic.Presentation.Services.Interfaces
{
    public interface IUsuarioLogadoService
    {
        int? UsuarioLogadoId {  get; }
        bool UsuarioMaster { get; }
        string TipoUsuario { get; }
        int? ClienteId { get; }
        string? NomeCliente { get; }
        int? NichoClienteId { get; }
        string? Email { get; }
    }
}
