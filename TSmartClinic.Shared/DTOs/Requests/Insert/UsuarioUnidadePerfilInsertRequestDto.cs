namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class UsuarioUnidadePerfilInsertRequestDto
    {
        public int UnidadeId { get; set; }
        public int PerfilId { get; set; }
        public bool UnidadePadrao { get; set; }
    }
}
