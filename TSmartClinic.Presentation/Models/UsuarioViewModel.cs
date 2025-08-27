using TSmartClinic.Shared.DTOs.Requests.Update;

namespace TSmartClinic.Presentation.Models
{
    public class UsuarioViewModel : BaseViewModel
    {
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; } = DateTime.UtcNow;
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.UtcNow;
        public DateTime? DataBloqueio { get; set; } 
        public DateTime? DataUltimoAcesso { get; set; } = DateTime.UtcNow;
        public DateTime? DataExpiracaoSenha { get; set; } = DateTime.UtcNow.AddDays(365);
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public char? TipoUsuario { get; set; } = 'C';
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; } 
        public bool Ativo { get; set; } = true;
        public bool PrimeiroAcesso { get; set; }
        public int ClienteId { get; set; }
        public List<ClienteViewModel>? ListClientes { get; set; }
        public List<UsuarioClientePerfilViewModel>? UsuarioClientePerfil { get; set; }
        // public List<PerfilViewModel>? PerfisCliente { get; set; }
        public int? PerfilClienteId { get; set; }
    }
}
