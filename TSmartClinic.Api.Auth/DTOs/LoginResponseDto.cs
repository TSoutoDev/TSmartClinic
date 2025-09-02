using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Api.Auth.DTOs
{
    public class LoginResponseDto
    {
 
        public string AccessToken { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? IdUsuario { get; set; }
        public string? TipoUsuario { get; set; }
        public bool PrimeiroAcesso { get; set; } = false;
        public List<Cliente>? ListClientes { get; set; }

    }
}
