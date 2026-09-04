using TSmartClinic.Shared.DTOs.Requests.Update;

namespace TSmartClinic.Presentation.Models
{
    public class UsuarioViewModel : BaseViewModel
    {
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTimeOffset? DataInclusao { get; set; }
        public string? LoginAlteracao { get; set; }
        public DateTimeOffset? DataAlteracao { get; set; } 
        public DateTimeOffset? DataBloqueio { get; set; } 
        public DateTimeOffset? DataUltimoAcesso { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DataExpiracaoSenha { get; set; } = DateTimeOffset.UtcNow.AddDays(365);
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public char? TipoUsuario { get; set; } = 'C';
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; } 
        public bool Ativo { get; set; } = true;
        public bool PrimeiroAcesso { get; set; }
        public int ClienteId { get; set; }
        public int? UnidadeId { get; set; }
        public int? PerfilClienteId { get; set; }
        public string? NomePerfil { get; set; }
        
        public List<ClienteViewModel>? ListClientes { get; set; }
        public List<UsuarioUnidadePerfilViewModel>? UsuarioUnidadePerfil { get; set; }

        // public List<PerfilViewModel>? PerfisCliente { get; set; }
    
    }
}
