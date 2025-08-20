using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class UsuarioInsertRequestDTO : BaseUsuarioRequestDTO
    {
        public string? Email { get; set; }
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public bool PrimeiroAcesso { get; set; } = true;
        public List<UsuarioClientePerfilInsertRequestDto>? UsuarioClientePerfil {  get; set; }
    }
}


