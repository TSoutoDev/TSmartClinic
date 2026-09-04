using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TSmartClinic.Api.Auth.DTOs;
using TSmartClinic.Api.Auth.Interfaces.Services;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Exceptions;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Models;
using TSmartClinic.Core.Infra.Security.Services;

namespace TSmartClinic.Api.Auth.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioService? _usuarioService;
        private readonly IUsuarioClientePerfilService? _usuarioClinicaPerfilService;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUsuarioUnidadePerfilService _usuarioUnidadePerfilService;
        public AutenticacaoService(IUsuarioUnidadePerfilService usuarioUnidadePerfilService, IUsuarioService? usuarioService, IUsuarioClientePerfilService usuarioClinicaPerfil, ICriptografiaProvider criptografiaProvider = null, ITokenService tokenService = null, IMapper mapper = null, IHttpContextAccessor httpContextAccessor = null)
        {
            _usuarioUnidadePerfilService = usuarioUnidadePerfilService;
            _usuarioService = usuarioService;
            _usuarioClinicaPerfilService = usuarioClinicaPerfil;
            _criptografiaProvider = criptografiaProvider;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginResponseDto? Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                if (loginRequestDto == null)
                    throw new ArgumentNullException(nameof(loginRequestDto), "A requisição de login não pode ser nula.");

                var email = loginRequestDto.Email?.Trim().ToLowerInvariant();
                var senha = loginRequestDto.Senha;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                    throw new ArgumentException("E-mail e senha são obrigatórios.");

                var usuario = _usuarioService?.ObterPorEmail(email);

                if (usuario == null)
                    return null;

                if (string.IsNullOrEmpty(usuario.Senha))
                {
                    return new LoginResponseDto
                    {
                        PrimeiroAcesso = true,
                        Email = usuario.Email,
                        Nome = usuario.Nome,
                        IdUsuario = usuario.Id
                    };
                }

                var hasher = new PasswordHasher<Usuario>();
                var vr = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);

                if (vr != PasswordVerificationResult.Success &&
                    vr != PasswordVerificationResult.SuccessRehashNeeded)
                    return null;

                var usuarioAutenticacao = _mapper.Map<AutenticacaoModel>(usuario);
                usuarioAutenticacao.Id = usuario.Id;
                usuarioAutenticacao.TipoUsuario = usuario.TipoUsuario;

                var unidadesUsuario = _usuarioUnidadePerfilService.ObterUnidadesDoUsuario(usuario.Id);
                var unidadePadrao = _usuarioUnidadePerfilService.ObterUnidadePadraoDoUsuario(usuario.Id);

                if (unidadesUsuario == null || !unidadesUsuario.Any())
                    throw new ApplicationException("Usuário não possui nenhuma unidade vinculada.");

                var necessitaSelecionarUnidade = unidadesUsuario.Count > 1 && unidadePadrao == null;
                var unidadeSelecionada = unidadePadrao ?? (unidadesUsuario.Count == 1 ? unidadesUsuario.First() : null);

                if (unidadeSelecionada != null)
                {
                    usuarioAutenticacao.UnidadeId = unidadeSelecionada.Id;
                    usuarioAutenticacao.ClienteId = unidadeSelecionada.ClienteId;
                    usuarioAutenticacao.ClienteNichoId = unidadeSelecionada.Cliente?.NichoId;
                }

                var clientesUsuario = unidadesUsuario
                    .Where(u => u.Cliente != null)
                    .Select(u => u.Cliente!)
                    .GroupBy(c => c.Id)
                    .Select(g => g.First())
                    .ToList();

                string? accessToken = null;
                string? tokenSelecaoUnidade = null;
                var permissoes = new List<string>();

                if (necessitaSelecionarUnidade)
                {
                    tokenSelecaoUnidade = _tokenService.GerarTokenSelecaoUnidade(usuario.Id);
                }
                else if (unidadeSelecionada != null)
                {
                    var perfilId = _usuarioUnidadePerfilService.ObterPerfilIdPorUsuarioUnidade(usuario.Id, unidadeSelecionada.Id);

                    if (!perfilId.HasValue)
                        throw new AcessoNegadoException("Usuário não possui perfil vinculado à unidade selecionada.");

                    permissoes = _usuarioService.ObterPermissoesPorPerfil(perfilId.Value);
                    accessToken = _tokenService.GerarToken(usuarioAutenticacao);
                }

                return new LoginResponseDto
                {
                    AccessToken = accessToken,
                    TokenSelecaoUnidade = tokenSelecaoUnidade,

                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    IdUsuario = usuarioAutenticacao.Id,
                    TipoUsuario = usuarioAutenticacao.TipoUsuario?.ToString(),

                    UnidadeId = unidadeSelecionada?.Id,
                    NecessitaSelecionarUnidade = necessitaSelecionarUnidade,

                    ListClientes = clientesUsuario
                        .Select(c => new LoginClienteDto
                        {
                            Id = c.Id,
                            PublicId = c.PublicId,
                            NomeCliente = c.NomeCliente,
                            RazaoSocial = c.RazaoSocial,
                            Cnpj = c.Cnpj,
                            NichoId = c.NichoId
                        })
                        .ToList(),

                    Unidades = unidadesUsuario
                        .Select(u => new UnidadeLoginDto
                        {
                            Id = u.Id,
                            PublicId = u.PublicId,
                            NomeUnidade = u.NomeUnidade,
                            ClienteId = u.ClienteId,
                            UnidadePadrao = unidadePadrao != null && unidadePadrao.Id == u.Id
                        })
                        .ToList(),

                    PrimeiroAcesso = usuario.PrimeiroAcesso,
                    Permissoes = permissoes
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

        public LoginResponseDto? SelecionarUnidade(SelecionarUnidadeRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.TokenSelecaoUnidade))
                throw new UnauthorizedAccessException("Token de seleção de unidade não informado.");

            var principal = _tokenService.ValidarToken(request.TokenSelecaoUnidade);
            var purpose = principal.FindFirst("purpose")?.Value;

            if (!string.Equals(purpose, "select_unit", StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Token inválido para seleção de unidade.");

            var usuarioIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out var usuarioId))
                throw new UnauthorizedAccessException("Usuário inválido no token de seleção.");

            var possuiAcesso = _usuarioUnidadePerfilService.UsuarioPossuiAcessoUnidade(usuarioId, request.UnidadeId);

            if (!possuiAcesso)
                throw new AcessoNegadoException("Usuário não possui acesso à unidade selecionada.");

            var usuario = _usuarioService?.ObterPorId(usuarioId);

            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado.");

            var unidadesUsuario = _usuarioUnidadePerfilService.ObterUnidadesDoUsuario(usuarioId);
            var unidadeSelecionada = unidadesUsuario.FirstOrDefault(x => x.Id == request.UnidadeId);

            if (unidadeSelecionada == null)
                throw new AcessoNegadoException("Unidade selecionada não encontrada.");

            if (unidadeSelecionada.Cliente == null)
                throw new ApplicationException("Cliente da unidade não encontrado.");

            var clienteSelecionado = unidadeSelecionada.Cliente;
            var perfilId = _usuarioUnidadePerfilService.ObterPerfilIdPorUsuarioUnidade(usuarioId, unidadeSelecionada.Id);

            if (!perfilId.HasValue)
                throw new AcessoNegadoException("Usuário não possui perfil vinculado à unidade selecionada.");

            if (request.DefinirComoPadrao)
                _usuarioUnidadePerfilService.DefinirUnidadePadrao(usuarioId, unidadeSelecionada.Id);

            var usuarioAutenticacao = _mapper.Map<AutenticacaoModel>(usuario);
            usuarioAutenticacao.Id = usuario.Id;
            usuarioAutenticacao.TipoUsuario = usuario.TipoUsuario;
            usuarioAutenticacao.UnidadeId = unidadeSelecionada.Id;
            usuarioAutenticacao.ClienteId = unidadeSelecionada.ClienteId;
            usuarioAutenticacao.ClienteNichoId = clienteSelecionado.NichoId;

            var permissoes = _usuarioService.ObterPermissoesPorPerfil(perfilId.Value);
            var accessToken = _tokenService.GerarToken(usuarioAutenticacao);
            var unidadePadrao = _usuarioUnidadePerfilService.ObterUnidadePadraoDoUsuario(usuarioId);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                TokenSelecaoUnidade = null,
                Nome = usuario.Nome,
                Email = usuario.Email,
                IdUsuario = usuario.Id,
                TipoUsuario = usuario.TipoUsuario.ToString(),
                UnidadeId = unidadeSelecionada.Id,
                NecessitaSelecionarUnidade = false,

                ListClientes = unidadesUsuario
                    .Where(x => x.Cliente != null)
                    .Select(x => x.Cliente!)
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    .Select(c => new LoginClienteDto
                    {
                        Id = c.Id,
                        PublicId = c.PublicId,
                        NomeCliente = c.NomeCliente,
                        RazaoSocial = c.RazaoSocial,
                        Cnpj = c.Cnpj,
                        NichoId = c.NichoId
                    })
                    .ToList(),

                Unidades = unidadesUsuario
                    .Select(u => new UnidadeLoginDto
                    {
                        Id = u.Id,
                        PublicId = u.PublicId,
                        NomeUnidade = u.NomeUnidade,
                        ClienteId = u.ClienteId,
                        UnidadePadrao = unidadePadrao != null && unidadePadrao.Id == u.Id
                    })
                    .ToList(),

                PrimeiroAcesso = usuario.PrimeiroAcesso,
                Permissoes = permissoes
            };
        }

        public LoginResponseDto? TrocarUnidade(int usuarioId, TrocarUnidadeRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var possuiAcesso = _usuarioUnidadePerfilService.UsuarioPossuiAcessoUnidade(usuarioId, request.UnidadeId);

            if (!possuiAcesso)
                throw new AcessoNegadoException("Usuário não possui acesso à unidade selecionada.");

            var usuario = _usuarioService?.ObterPorId(usuarioId);

            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado.");

            var unidadesUsuario = _usuarioUnidadePerfilService.ObterUnidadesDoUsuario(usuarioId);
            var unidadeSelecionada = unidadesUsuario.FirstOrDefault(x => x.Id == request.UnidadeId);

            if (unidadeSelecionada == null)
                throw new AcessoNegadoException("Unidade selecionada não encontrada.");

            if (unidadeSelecionada.Cliente == null)
                throw new ApplicationException("Cliente da unidade não encontrado.");

            var clienteSelecionado = unidadeSelecionada.Cliente;

            var perfilId = _usuarioUnidadePerfilService.ObterPerfilIdPorUsuarioUnidade(usuarioId, unidadeSelecionada.Id);

            if (!perfilId.HasValue)
                throw new AcessoNegadoException("Usuário não possui perfil vinculado à unidade selecionada.");

            var usuarioAutenticacao = _mapper.Map<AutenticacaoModel>(usuario);
            usuarioAutenticacao.Id = usuario.Id;
            usuarioAutenticacao.TipoUsuario = usuario.TipoUsuario;
            usuarioAutenticacao.UnidadeId = unidadeSelecionada.Id;
            usuarioAutenticacao.ClienteId = unidadeSelecionada.ClienteId;
            usuarioAutenticacao.ClienteNichoId = clienteSelecionado.NichoId;

            var permissoes = _usuarioService.ObterPermissoesPorPerfil(perfilId.Value);
            var accessToken = _tokenService.GerarToken(usuarioAutenticacao);
            var unidadePadrao = _usuarioUnidadePerfilService.ObterUnidadePadraoDoUsuario(usuarioId);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                TokenSelecaoUnidade = null,

                Nome = usuario.Nome,
                Email = usuario.Email,
                IdUsuario = usuario.Id,
                TipoUsuario = usuario.TipoUsuario.ToString(),

                UnidadeId = unidadeSelecionada.Id,
                NecessitaSelecionarUnidade = false,

                ListClientes = unidadesUsuario
                    .Where(x => x.Cliente != null)
                    .Select(x => x.Cliente!)
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    .Select(c => new LoginClienteDto
                    {
                        Id = c.Id,
                        PublicId = c.PublicId,
                        NomeCliente = c.NomeCliente,
                        RazaoSocial = c.RazaoSocial,
                        Cnpj = c.Cnpj,
                        NichoId = c.NichoId
                    })
                    .ToList(),

                Unidades = unidadesUsuario
                    .Select(u => new UnidadeLoginDto
                    {
                        Id = u.Id,
                        PublicId = u.PublicId,
                        NomeUnidade = u.NomeUnidade,
                        ClienteId = u.ClienteId,
                        UnidadePadrao = unidadePadrao != null && unidadePadrao.Id == u.Id
                    })
                    .ToList(),

                PrimeiroAcesso = usuario.PrimeiroAcesso,
                Permissoes = permissoes
            };
        }
    }
}
