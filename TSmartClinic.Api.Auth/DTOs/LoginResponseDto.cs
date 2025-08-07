using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Api.Auth.DTOs
{
    public class LoginResponseDto
    {
 
        public string AccessToken { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? IdUsuario { get; set; }
        public string? TipoUsuario { get; set; }
        public List<Cliente>? ListClientes { get; set; }

    }
}
