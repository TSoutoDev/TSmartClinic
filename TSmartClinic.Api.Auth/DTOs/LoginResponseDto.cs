using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Api.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public List<Cliente>? ListClinicas { get; set; }

    }
}
