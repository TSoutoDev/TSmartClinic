using TSmartClinic.Shared.DTOs.Requests.Insert;
using TSmartClinic.Shared.DTOs.Requests.Update;

namespace TSmartClinic.Shared.DTOs.Responses
{
    public class UsuarioResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? Senha { get; set; }
        public string? Nome { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataBloqueio { get; set; }
        public DateTime? DataUltimoAcesso { get; set; }
        public DateTime? DataExpiracaoSenha { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public char? TipoUsuario { get; set; }
        public byte[]? Foto { get; set; }
        public bool FlagBloqueado { get; set; }
        public bool Ativo { get; set; }
        public int ClienteId { get; set; }
        public string? NomePerfil { get; set; }
        public List<UsuarioClientePerfilUpdateRequestDto>? UsuarioClientePerfil { get; set; }
        //public Cliente? Cliente { get; set; } = null!; // Navegação para Cliente
    }
}
