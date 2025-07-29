namespace TSmartClinic.Api.Auth.DTOs
{
    public class UsuarioClinicaPerfilRequestDto
    {
        public int UsuarioId { get; set; }
        public int ClinicaId { get; set; }
        public int PerfilId { get; set; }

        public bool ClinicaPadrao { get; set; }
    }
}
