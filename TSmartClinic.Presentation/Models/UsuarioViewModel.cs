﻿namespace TSmartClinic.Presentation.Models
{
    public class UsuarioViewModel : BaseViewModel
    {
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; } = DateTime.UtcNow;
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.UtcNow;
        public DateTime? DataBloqueio { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltimoAcesso { get; set; } = DateTime.UtcNow;
        public DateTime? DataExpiracaoSenha { get; set; } = DateTime.UtcNow.AddDays(365);
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? Cliente { get; set; }
        public char? TipoUsuario { get; set; }
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; }
        public bool Ativo { get; set; }
    }
}
