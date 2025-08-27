namespace TSmartClinic.Presentation.Models
{
    public class UsuarioClientePerfilViewModel
    {
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public int PerfilId { get; set; }
        public bool ClientePadrao { get; set; }

        public ClienteViewModel? Cliente { get; set; }
        public PerfilViewModel? Perfil { get; set; }
    }
}
