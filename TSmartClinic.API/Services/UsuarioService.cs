using System.Runtime.InteropServices;
using TSmartClinic.API.Repositories;
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
        private readonly IEmailService _emailService;
        private readonly EmailQueue _emailQueue;

        public UsuarioService(IUsuarioClientePerfilRepository usuarioClientePerfilRepository,
                                IUsuarioLogadoService usuarioLogadoService,
                                EmailQueue emailQueue,
                                IPerfilRepository perfilRepository,
                                IUsuarioRepository usuarioRepository,
                                ICriptografiaProvider criptografiaProvider = null,
                                IEmailService emailService = null ) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _criptografiaProvider = criptografiaProvider;
            _usuarioLogadoService = usuarioLogadoService;
            _usuarioClientePerfilRepository = usuarioClientePerfilRepository;
            _emailService = emailService;
            _emailQueue = emailQueue;
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
            usuario.Senha = _criptografiaProvider.Criptografar(usuario.Senha);

            if (_usuarioLogadoService.UsuarioMaster)
            {
                _perfilRepository.ListarTodos();
            }

            // guarda a lista antes
            var perfis = usuario.UsuarioClientePerfil?.ToList();

            // zera pra não tentar salvar tudo junto
            usuario.UsuarioClientePerfil = null;
            usuario.PrimeiroAcesso = true;

            // insere só o usuario
           var usuarioGravado = _usuarioRepository?.Inserir(usuario);

            if (perfis != null)
            {
                foreach (var p in perfis)
                {
                    p.UsuarioId = usuarioGravado.Id;
                    _usuarioClientePerfilRepository.Inserir(p);
                }

               usuario.UsuarioClientePerfil = perfis;
            }
            // --- Envio de e-mail via fila ---
            string corpoEmail = $@"
                <h2>Bem-vindo ao sistema!</h2>
                <p>Seu usuário foi criado com sucesso.</p>
                <p><strong>Login de acesso:</strong> {usuario.Email}</p>
                <p><strong>Senha temporária:</strong> {_criptografiaProvider.Decriptografar(usuario.Senha)}</p>
                <p>No primeiro acesso você deverá alterar a senha para uma de sua preferência.</p>
                <p>Acesse o sistema aqui: <a href='https://meusistema.com/login'>Login</a></p>
            ";

            // Enfileira o e-mail para ser processado em background
            _emailQueue.Enqueue(usuario.Email, "Acesso ao sistema", corpoEmail);
            return  usuario;
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

        public void DefinirSenhaPrimeiroAcesso(int usuarioId, string novaSenha)
        {
            _usuarioRepository.DefinirSenhaPrimeiroAcesso(usuarioId, novaSenha);
        }
    }
}
