using System.Text.Json.Serialization;
using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class UsuarioInsertRequestDTO : BaseUsuarioRequestDTO
    {
        public string? LoginInclusao { get; set; }

        [JsonIgnore] // o cliente não controla isso
        public DateTime? DataInclusao { get; set; } = DateTime.UtcNow;
        public bool PrimeiroAcesso { get; set; } = true;
        public List<UsuarioClientePerfilInsertRequestDto>? UsuarioClientePerfil {  get; set; }
    }
}


