using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Update
{
    public class CategoriaUpdateRequestDTO : BaseCategoriaRequestDTO
    {
        public int Id { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.UtcNow;
        public string? UsuarioAlteracao { get; set; }
    }
}


