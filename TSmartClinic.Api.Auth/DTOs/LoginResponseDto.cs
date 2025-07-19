namespace TSmartClinic.Api.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int ClinicaId { get; set; }
        public int ModuloId { get; set; }
    }
}
