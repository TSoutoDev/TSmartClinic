using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Models;
using TSmartClinic.Core.Infra.Security.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TSmartClinic.Core.Infra.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings? _tokenSettings;
        public TokenService(IOptions<TokenSettings>? tokenSettings)
        {
            _tokenSettings = tokenSettings?.Value;
        }
        public string GerarRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                                        .Replace("+", string.Empty)
                                        .Replace("/", string.Empty)
                                        .Replace("=", string.Empty);
        }

        public string GerarToken(AutenticacaoModel autenticacao, List<string> permissoes)
        {
            if (_tokenSettings == null || string.IsNullOrWhiteSpace(_tokenSettings.SecretKey))
                throw new InvalidOperationException("Configuração de TokenSettings inválida.");

            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, autenticacao.Nome),
                    new Claim(ClaimTypes.Email, autenticacao.Email),
                    new Claim("permissao", string.Join(",", permissoes))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtSecurityToken);

            Console.WriteLine($"Token gerado: {token}");

            return token;
        }

        public string GerarTokenRedefinicaoSenha(string email)
        {
            throw new NotImplementedException();
        }

        public void InValidarRefreshToken(string refreshToken, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal ValidarToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
