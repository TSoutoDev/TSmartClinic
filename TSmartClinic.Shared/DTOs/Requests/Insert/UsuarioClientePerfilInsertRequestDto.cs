using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class UsuarioClientePerfilInsertRequestDto : BaseUsuarioClientePerfilRequestDto
    {
        public int ClienteId { get; set; }
        public int PerfilId { get; set; }
        public bool ClientePadrao { get; set; } = false;
    }
}
