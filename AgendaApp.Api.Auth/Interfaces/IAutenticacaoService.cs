using AgendaApp.Api.Auth.DTOs;
namespace AgendaApp.Api.Auth.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        LoginResponseDto Login(LoginRequestDto loginRequestDto);
        void Logout(int usuarioId);
        LoginResponseDto RefreshToken(int usuarioId);
    }
}
