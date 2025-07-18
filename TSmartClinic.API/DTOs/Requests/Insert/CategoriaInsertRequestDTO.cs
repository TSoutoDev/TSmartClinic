using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Insert
{
    public class CategoriaInsertRequestDTO : BaseCategoriaRequestDTO
    {
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow;
        public string? UsuarioCriacao { get; set; }
    }
}
