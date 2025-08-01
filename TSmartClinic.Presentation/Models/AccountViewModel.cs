namespace TSmartClinic.Presentation.Models
{
    public class AccountViewModel : BaseViewModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? AccessToken { get; set; }
        public List<ClinicaViewModel>? ListClinicas { get; set; }
    }
}
