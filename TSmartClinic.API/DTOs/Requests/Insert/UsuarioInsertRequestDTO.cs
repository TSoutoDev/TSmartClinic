using System.Security;
using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Insert
{
    public class UsuarioInsertRequestDTO : BaseUsuarioRequestDTO
    {
        public string? Email { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public bool PrimeiroAcesso { get; set; } = true;
    }
}
