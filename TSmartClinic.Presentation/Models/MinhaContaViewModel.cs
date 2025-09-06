namespace TSmartClinic.Presentation.Models
{
    public class MinhaContaViewModel : BaseViewModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public byte[]? Foto { get; set; }
        public string? NomePerfil { get; set; }
        public List<UsuarioClientePerfilViewModel>? UsuarioClientePerfil { get; set; }
    }
}
