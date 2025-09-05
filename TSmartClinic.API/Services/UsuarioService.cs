using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Core.Infra.CrossCutting.Email;
using TSmartClinic.Core.Infra.CrossCutting.Email.FilaEmails;

namespace TSmartClinic.API.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IUsuarioClientePerfilRepository? _usuarioClientePerfilRepository;
        private readonly IPerfilRepository? _perfilRepository;
        private readonly ICriptografiaProvider _criptografiaProvider;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly EmailQueue _emailQueue;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration; // injete no ctor

        public UsuarioService(IUsuarioClientePerfilRepository usuarioClientePerfilRepository,
                                IUsuarioLogadoService usuarioLogadoService,
                                EmailQueue emailQueue,
                                IPerfilRepository perfilRepository,
                                IUsuarioRepository usuarioRepository,
                                ITokenService tokenService,
                                IConfiguration configuration,
                                ICriptografiaProvider criptografiaProvider = null,
                                IEmailService emailService = null ) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _criptografiaProvider = criptografiaProvider;
            _usuarioLogadoService = usuarioLogadoService;
            _configuration = configuration;
            _usuarioClientePerfilRepository = usuarioClientePerfilRepository;
            _emailQueue = emailQueue;
            _tokenService = tokenService;
        }

        public void Bloquear(int id)
        {
            var usuario = _usuarioRepository?.ObterPorId(id);

            usuario?.Bloquear();

            _usuarioRepository?.Atualizar(usuario);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepository.ObterPorEmail(email);
        }

        public override Usuario Inserir(Usuario usuario)
        {
            // 👉 Não armazene senha reversível na criação
            // Se vier senha no DTO, ignore aqui e force o primeiro acesso via link
            usuario.Senha = null; // ou algum placeholder; ideal é null e a coluna permitir null até o primeiro acesso
            usuario.PrimeiroAcesso = true;

            if (_usuarioLogadoService.UsuarioMaster)
                _perfilRepository.ListarTodos();

            // guarda a lista antes e zera para evitar save em cascata indevido
            var perfis = usuario.UsuarioClientePerfil?.ToList();
            usuario.UsuarioClientePerfil = null;

            // 1) Persistir o usuário
            var usuarioGravado = _usuarioRepository!.Inserir(usuario);

            // 2) Persistir os perfis vinculados
            if (perfis != null && perfis.Count > 0)
            {
                foreach (var p in perfis)
                {
                    p.UsuarioId = usuarioGravado.Id;
                    _usuarioClientePerfilRepository!.Inserir(p);
                }
                usuarioGravado.UsuarioClientePerfil = perfis;
            }

            // 3) Gerar token de primeiro acesso (purpose=set_password)
            // preferível usar o ID, mas sua assinatura atual usa e-mail; ok também:
            //  var url = $"https://meusistema.com/alterar-senha?token={Uri.EscapeDataString(tokenPrimeiroAcesso)}";
            // var url = $"http://localhost:5041/primeiro-acesso?token={Uri.EscapeDataString(tokenPrimeiroAcesso)}";

            var tokenRedefinicao = _tokenService.GerarTokenRedefinicaoSenha(usuarioGravado.Email);
            var frontBaseUrl = _configuration["FrontSettings:BaseUrl"];
            var url = $"{frontBaseUrl}/account/primeiro-acesso?token={Uri.EscapeDataString(tokenRedefinicao)}";

            // 4) Enfileirar e-mail (sem bloquear o fluxo principal)
            var corpoEmail = $@"
                    <h2>Bem-vindo ao sistema!</h2>
                    <p>Seu usuário foi criado com sucesso.</p>
                    <p><strong>Login de acesso:</strong> {usuarioGravado.Email}</p>
                    <p>Para definir sua senha, clique no link abaixo (válido por 24 horas):</p>
                    <p><a href=""{url}"" style=""display:inline-block;padding:10px 16px;background:#1976d2;color:#fff;text-decoration:none;border-radius:6px"">
                       Definir minha senha</a></p>
                    <p>Se o botão não funcionar, copie e cole este link no navegador:</p>
                    <p>{url}</p>";

            try
            {
                _emailQueue.Enqueue(usuarioGravado.Email, "Defina sua senha de acesso", corpoEmail);
            }
            catch (Exception ex)
            {
                // logue mas não quebre a criação do usuário
                // _logger?.LogError(ex, "Falha ao enfileirar e-mail de primeiro acesso para {Email}", usuarioGravado.Email);
            }

            // 5) Retornar o objeto já persistido
            return usuarioGravado;
        }


        public override Usuario Atualizar(int id, Usuario usuario)
        {
            var usuarioExistente = _usuarioRepository?.ObterPorId(id);

            // Se a senha foi alterada, criptografar
            if (!string.Equals(usuario.Senha, usuarioExistente))
            {
                usuario.Senha = _criptografiaProvider.Criptografar(usuario.Senha);
            }
            // Atualizar o usuario
            var usuarioAtualizado = _usuarioRepository?.Atualizar(usuario);

            return base.Atualizar(id, usuario);
        }

        public List<string> ObterPermissaoUsuario(int usuarioId, List<Cliente> clinicasUsuario)
        {
            throw new NotImplementedException();
        }

        public void DefinirSenha(string token, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token é obrigatório.", nameof(token));

            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 6)
                throw new ArgumentException("A senha deve ter no mínimo 6 caracteres.", nameof(novaSenha));

            var principal = _tokenService.ValidarToken(token); // valida assinatura/issuer/audience/exp
            var purpose = principal.FindFirst("purpose")?.Value;

            if (purpose != "set_password" && purpose != "reset_password")
                throw new SecurityTokenException("Token não é válido para definir senha.");

            // tenta Sub, depois NameIdentifier, por fim "sub" literal
            var subClaim =
                principal.FindFirst(JwtRegisteredClaimNames.Sub) ??
                principal.FindFirst(ClaimTypes.NameIdentifier) ??
                principal.Claims.FirstOrDefault(c => c.Type == "sub");

            if (subClaim is null || !int.TryParse(subClaim.Value, out var usuarioId))
                throw new SecurityTokenException("Token inválido (sub).");

            var hasher = new PasswordHasher<object>();
            var hash = hasher.HashPassword(new object(), novaSenha);

            _usuarioRepository.AtualizarSenhaHash(usuarioId, hash);

        }

    }
}
