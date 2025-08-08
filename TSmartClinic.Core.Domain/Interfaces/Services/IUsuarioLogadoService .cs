namespace TSmartClinic.Core.Domain.Interfaces.Services
{
    public interface IUsuarioLogadoService
    {
       public int? UsuarioLogadoId { get; }
       public string TipoUsuario { get; }
       public bool UsuarioMaster { get; }
       public int? ClienteId { get; }
       public int? NichoClienteId { get; }
    }
}
