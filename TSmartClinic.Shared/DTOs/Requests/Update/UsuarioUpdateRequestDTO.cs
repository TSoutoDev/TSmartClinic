using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Update
{
    public class UsuarioUpdateRequestDTO : BaseUsuarioRequestDTO
    {
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.UtcNow;
        public List<UsuarioUnidadePerfilUpdateRequestDto>? UsuarioUnidadePerfil { get; set; }
    }
}

