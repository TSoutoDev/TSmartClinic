namespace TSmartClinic.Presentation.Models
{
    public class UsuarioUnidadePerfilViewModel
    {
        public int UnidadeId { get; set; }
        public int PerfilId { get; set; }
        public bool UnidadePadrao { get; set; }

        public UnidadeViewModel? Unidade { get; set; }
        public PerfilViewModel? Perfil { get; set; }
    }
}
