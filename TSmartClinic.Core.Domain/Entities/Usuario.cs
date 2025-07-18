using TSmartClinic.Core.Domain.Helpers;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Usuario : Base
    {
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; } 
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao{ get; set; }
        public DateTime? DataBloqueio { get; set; }
        public DateTime? DataUltimoAcesso { get; set; }
        public DateTime? DataExpiracaoSenha { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? Cliente { get; set; }
        public char? TipoUsuario { get; set; }
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; }
        public bool Ativo { get; set; }
        
       public void Bloquear()
        {
            this.FlagBloqueado = true;
            this.DataBloqueio = DateTime.Now;
        }

        public override void Atualizar(Object obj)
        {
            Usuario usuario = obj as Usuario;

            this.Login = usuario?.Login;
            this.Nome = usuario?.Nome;
            this.LoginAlteracao = usuario?.LoginAlteracao;
            this.DataAlteracao = DateTime.UtcNow;
            this.DataBloqueio = usuario.DataBloqueio;
            this.DataUltimoAcesso = usuario.DataUltimoAcesso;
            this.DataExpiracaoSenha = usuario.DataExpiracaoSenha;
            this.Email = usuario.Email;
            this.Celular = usuario.Celular;
            this.Cliente = usuario.Cliente;
            this.TipoUsuario = usuario.TipoUsuario;
            this.Foto = usuario.Foto;
            this.FlagBloqueado = usuario.FlagBloqueado;
            this.Ativo = usuario.Ativo;

            this.RemoverEspacosEmBranco();
        }
    }
}
