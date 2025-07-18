using TSmartClinic.Data.Entities;

namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BaseCategoriaRequestDTO : BaseRequestDTO
    {
        public string? Descricao { get; set; }
        public bool? FlagSituacao { get; set; }
    }
}
