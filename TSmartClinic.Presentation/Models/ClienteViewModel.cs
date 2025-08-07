namespace TSmartClinic.Presentation.Models
{
    public class ClienteViewModel : BaseViewModel
    {

        public string? NomeCliente { get; set; }
        public string? RazaoSocial { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? NichoId { get; set; }
    }
}
