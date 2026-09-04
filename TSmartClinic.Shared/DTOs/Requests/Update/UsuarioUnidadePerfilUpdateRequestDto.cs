namespace TSmartClinic.Shared.DTOs.Requests.Update
{
    public class UsuarioUnidadePerfilUpdateRequestDto
    {
        public int UnidadeId { get; set; }
        public int PerfilId { get; set; }
        public bool UnidadePadrao { get; set; }
    }
}