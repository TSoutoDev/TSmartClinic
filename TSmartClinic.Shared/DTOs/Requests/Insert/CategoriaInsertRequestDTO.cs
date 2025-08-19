using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class CategoriaInsertRequestDTO : BaseCategoriaRequestDTO
    {
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow;
        public string? UsuarioCriacao { get; set; }
    }
}


