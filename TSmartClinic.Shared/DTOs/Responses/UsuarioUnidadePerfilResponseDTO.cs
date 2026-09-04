namespace TSmartClinic.Shared.DTOs.Responses
{
    public class UsuarioUnidadePerfilResponseDTO
    {
        public int UnidadeId { get; set; }
        public int PerfilId { get; set; }
        public bool UnidadePadrao { get; set; }

        public UnidadeResponseDTO? Unidade { get; set; }
        public PerfilResponseDTO? Perfil { get; set; }
    }
}