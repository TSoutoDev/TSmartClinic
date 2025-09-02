namespace TSmartClinic.Presentation.Models
{
    public class AccountViewModel : BaseViewModel
    {
        public string? AccessToken { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public int? IdUsuario { get; set; }
        public string? TipoUsuario { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public List<ClienteViewModel>? ListClientes { get; set; }
    }
}
