using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Insert
{
    public class UsuarioInsertRequestDTO : BaseUsuarioRequestDTO
    {
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; }
    }
}
