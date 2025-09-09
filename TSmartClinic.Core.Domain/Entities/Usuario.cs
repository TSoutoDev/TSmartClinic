using TSmartClinic.Core.Domain.Helpers;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Usuario : Base
    {
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; } = DateTime.UtcNow;
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao{ get; set; }
        public DateTime? DataBloqueio { get; set; } 
        public DateTime? DataUltimoAcesso { get; set; } = null;
        public DateTime? DataExpiracaoSenha { get; set; } = DateTime.UtcNow.AddDays(365);
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
            this.DataBloqueio = DateTime.Now;
        }

        public void DefinirSenhaPrimeiroAcesso(string senhaCriptografada)
        {
            this.Senha = senhaCriptografada;
            this.PrimeiroAcesso = false; // já redefiniu a senha
            this.DataAlteracao = DateTime.Now;
        }
        public override void Atualizar(Object obj)
        {
            Usuario usuario = obj as Usuario;

            this.Nome = usuario?.Nome;
            this.LoginAlteracao = usuario?.LoginAlteracao;
            this.DataAlteracao = DateTime.UtcNow;
            this.DataBloqueio = usuario.DataBloqueio;
            this.DataUltimoAcesso = usuario.DataUltimoAcesso;
            this.DataExpiracaoSenha = usuario.DataExpiracaoSenha;
            this.Email = usuario.Email;
            this.Celular = usuario.Celular;
            this.TipoUsuario = usuario.TipoUsuario;
            this.Foto = usuario.Foto;
            this.FlagBloqueado = usuario.FlagBloqueado;
            this.Ativo = usuario.Ativo;
            this.PrimeiroAcesso = usuario.PrimeiroAcesso;
            this.ClienteId = usuario.ClienteId;


            this.UsuarioClientePerfil = usuario.UsuarioClientePerfil
                 .Select(e => new UsuarioClientePerfil
                 {
                     UsuarioId = e.UsuarioId,
                     PerfilId = e.PerfilId,
                     ClienteId = e.ClienteId,
                     ClientePadrao = e.ClientePadrao
                 })
                 .ToList();

            this.RemoverEspacosEmBranco();
        }
    }
}
