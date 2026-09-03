using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Api.Auth.DTOs
{
    public class LoginResponseDto
    {
 
        public string? AccessToken { get; set; }
        public string? TokenSelecaoUnidade { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? IdUsuario { get; set; }
        public string? TipoUsuario { get; set; }
        public bool PrimeiroAcesso { get; set; } = false;
        public int? UnidadeId { get; set; }
        public bool NecessitaSelecionarUnidade { get; set; }

        public List<LoginClienteDto> ListClientes { get; set; } = new();
        public List<string> Permissoes { get; set; } = new(); 
        public List<UnidadeLoginDto> Unidades { get; set; } = new();
    }
}
