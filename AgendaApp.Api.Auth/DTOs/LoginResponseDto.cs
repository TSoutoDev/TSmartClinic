namespace AgendaApp.Api.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
