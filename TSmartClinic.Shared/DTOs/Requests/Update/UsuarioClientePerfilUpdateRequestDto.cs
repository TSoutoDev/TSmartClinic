using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Update
{
    public class UsuarioClientePerfilUpdateRequestDto : BaseUsuarioClientePerfilRequestDto
    {
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public int PerfilId { get; set; }
        public bool ClientePadrao { get; set; } = false;

    }
}
