namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseUsuarioRequestDTO : BaseRequestDTO
    {
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataBloqueio { get; set; }
        public DateTime? DataUltimoAcesso { get; set; } = DateTime.UtcNow;
        public DateTime? DataExpiracaoSenha { get; set; } = DateTime.UtcNow.AddDays(365);
        public string? Celular { get; set; }
        public char? TipoUsuario { get; set; }
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; } = false;
        public bool Ativo { get; set; }
        public int ClienteId { get; set; }

    }
}

