using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Models;
using TSmartClinic.Core.Infra.Security.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Repositories;

namespace TSmartClinic.Core.Infra.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings? _tokenSettings;
        private readonly IUsuarioRepository? _usuarioRepository;
        public TokenService(IUsuarioRepository usuarioRepository, IOptions<TokenSettings>? tokenSettings)
        {
            _tokenSettings = tokenSettings?.Value;
            _usuarioRepository = usuarioRepository;
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

            // 1. Buscar o usuário com cliente carregado
            var usuario = _usuarioRepository.ObterPorId(autenticacao.Id, x => x.Cliente);

            var nichoId = usuario.Cliente != null ? usuario.Cliente.NichoId : 0;

            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, autenticacao.Nome),
                    new Claim(ClaimTypes.Email, autenticacao.Email),
                    new Claim("permissao", string.Join(",", permissoes)),
                    
                    // Claims que o UsuarioLogadoService precisa
                    new Claim("Usuario_Id", autenticacao.Id.ToString() ?? ""),
                    new Claim("Cliente_Id", autenticacao.ClienteId.ToString() ?? ""),
                    new Claim("Cliente_NichoId", nichoId.ToString() ?? ""),
                    new Claim("TipoUsuario", autenticacao.TipoUsuario.ToString() ?? ""), // "M" para master, por exemplo
                    new Claim("Usuario_Email", autenticacao.Email.ToString() ?? "") // "M" para master, por exemplo
                  
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
            if (_tokenSettings == null || string.IsNullOrWhiteSpace(_tokenSettings.SecretKey))
                throw new InvalidOperationException("TokenSettings inválido.");

            var usuario = _usuarioRepository.ObterPorEmail(email)
                ?? throw new InvalidOperationException("Usuário não encontrado.");

            var jti = Guid.NewGuid().ToString("N");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("purpose", "set_password"),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // se for usar uso-único
            //_conviteRepo.Salvar(new ConviteSenha { UsuarioId = usuario.Id, Jti = jti, ExpiraEmUtc = DateTime.UtcNow.AddHours(24) });

            return tokenString;
        }

        // dentro de TokenService
        public ClaimsPrincipal ValidarToken(string token)
        {
            if (_tokenSettings == null || string.IsNullOrWhiteSpace(_tokenSettings.SecretKey))
                throw new InvalidOperationException("TokenSettings inválido.");

            var handler = new JwtSecurityTokenHandler();
            var parms = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _tokenSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };

            try { return handler.ValidateToken(token, parms, out _); }
            catch (Exception ex) { throw new SecurityTokenException("Token inválido ou expirado.", ex); }
        }

        public void InValidarRefreshToken(string refreshToken, int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
