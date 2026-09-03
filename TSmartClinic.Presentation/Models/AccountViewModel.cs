namespace TSmartClinic.Presentation.Models
{
    public class AccountViewModel : BaseViewModel
    {
        public string? AccessToken { get; set; }
        public string? TokenSelecaoUnidade { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public int? IdUsuario { get; set; }
        public int? UnidadeId { get; set; }
        public string? TipoUsuario { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public bool NecessitaSelecionarUnidade { get; set; }
        public List<ClienteViewModel>? ListClientes { get; set; }
        public List<string> Permissoes { get; set; } = new();         
        public List<UnidadeLoginViewModel> Unidades { get; set; } = new();
    }
}
