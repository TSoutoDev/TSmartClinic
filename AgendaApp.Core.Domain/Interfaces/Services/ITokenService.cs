using AgendaApp.Core.Domain.Models;
using System.Security.Claims;

namespace AgendaApp.Core.Domain.Interfaces.Services
{
    public  interface ITokenService
    {
        string GerarToken(AutenticacaoModel autenticacao, List<string> permissoes);
        string GerarTokenRedefinicaoSenha(string email);
        string GerarRefreshToken();
        //Autenticacao ValidarRefreshToken(string refreshToken, int usuarioId);
        void InValidarRefreshToken(string refreshToken, int usuarioId);
        ClaimsPrincipal ValidarToken(string token);
    }
}
