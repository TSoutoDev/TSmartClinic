namespace TSmartClinic.Shared.DTOs.Responses
{
    public class UsuarioClientePerfilResponseDto : BaseResponseDTO
    {
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public int PerfilId { get; set; }
        public bool ClientePadrao { get; set; }
    }
}
