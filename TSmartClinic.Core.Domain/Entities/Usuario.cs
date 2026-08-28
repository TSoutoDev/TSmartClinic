using TSmartClinic.Core.Domain.Helpers;
using TSmartClinic.Core.Domain.Interfaces.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Usuario : Base, IEntidadeComPublicId
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; } = DateTime.UtcNow;
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao{ get; set; }
        public DateTime? DataBloqueio { get; set; } 
        public DateTime? DataUltimoAcesso { get; set; } = null;
        public DateTime? DataExpiracaoSenha { get; set; } 
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public char? TipoUsuario { get; set; }
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; }
        public bool Ativo { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public int ClienteId {  get; set; } 
        public virtual  Cliente? Cliente { get; set; } = null!; // Navegação para Cliente
        public virtual  List<UsuarioClientePerfil>? UsuarioClientePerfil { get; set; } = new();      // Relação com UsuarioClientePerfil

        public void Bloquear()
        {
            this.FlagBloqueado = true;
            this.DataBloqueio = DateTime.UtcNow;
        }

        public void DefinirSenhaPrimeiroAcesso(string senhaCriptografada)
        {
            this.Senha = senhaCriptografada;
            this.PrimeiroAcesso = false; // já redefiniu a senha
            this.DataAlteracao = DateTime.UtcNow;
        }
        public override void Atualizar(object obj)
        {
            if (obj is not Usuario usuario)
                return;

            Nome = usuario.Nome;
            LoginAlteracao = usuario.LoginAlteracao;
            DataAlteracao = DateTime.UtcNow;

            DataBloqueio = usuario.DataBloqueio;
            DataExpiracaoSenha = usuario.DataExpiracaoSenha;

            Email = usuario.Email;
            Celular = usuario.Celular;
            TipoUsuario = usuario.TipoUsuario;

            Foto = usuario.Foto;
            FlagBloqueado = usuario.FlagBloqueado;
            Ativo = usuario.Ativo;
            PrimeiroAcesso = usuario.PrimeiroAcesso;
            ClienteId = usuario.ClienteId;

            // Só altera a senha se uma nova senha vier preenchida
            if (!string.IsNullOrWhiteSpace(usuario.Senha))
                Senha = usuario.Senha;

            UsuarioClientePerfil = usuario.UsuarioClientePerfil?
                .Select(e => new UsuarioClientePerfil
                {
                    PerfilId = e.PerfilId,
                    ClienteId = e.ClienteId,
                    ClientePadrao = e.ClientePadrao
                })
                .ToList() ?? new List<UsuarioClientePerfil>();

            this.RemoverEspacosEmBranco();
        }
    }
}
