using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Api.Auth.Interfaces.Services;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Models;
using AutoMapper;

namespace TSmartClinic.Api.Auth.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioService? _usuarioService;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AutenticacaoService(IUsuarioService? usuarioService, ICriptografiaProvider criptografiaProvider = null, ITokenService tokenService = null, IMapper mapper = null, IHttpContextAccessor httpContextAccessor = null)
        {
            _usuarioService = usuarioService;
            _criptografiaProvider = criptografiaProvider;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public LoginResponseDto Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                if (loginRequestDto == null)
                    throw new ArgumentNullException(nameof(loginRequestDto), "A requisição de login não pode ser nula.");

                if (string.IsNullOrWhiteSpace(loginRequestDto.Email) || string.IsNullOrWhiteSpace(loginRequestDto.Senha))
                    throw new ArgumentException("E-mail e senha são obrigatórios.");

                var usuario = _usuarioService?.ObterPorEmail(loginRequestDto.Email);

                if (usuario == null)
                    return null;

                var senhaDecifrada = _criptografiaProvider.Decriptografar(usuario.Senha);

                if (!senhaDecifrada.Equals(loginRequestDto.Senha))
                    return null;

                var usuarioAutenticacao = _mapper.Map<AutenticacaoModel>(usuario);

                //var usuarioSistema = usuario.UsuariosSistema.FirstOrDefault(); REVER
               var permissoes = _usuarioService.ObterPermissaoUsuario(usuario.Id, loginRequestDto.ClinicaId, loginRequestDto.ModuloId);

                var accessToken = _tokenService.GerarToken(usuarioAutenticacao, permissoes);

                return new LoginResponseDto
                {
                    AccessToken = accessToken,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                };

            }
            catch (AcessoNegadoException adx)
            {
                throw new ApplicationException("Acesso negado: " + adx.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao realizar login: " + ex.Message);
            }
        }

        public void Logout(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public LoginResponseDto RefreshToken(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
