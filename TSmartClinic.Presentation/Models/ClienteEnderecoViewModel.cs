namespace TSmartClinic.Presentation.Models
{
    public class ClienteEnderecoViewModel
    {
        public int? ClienteId { get; set; }
        public int? EnderecoId { get; set; }
        public string? Tipo { get; set; }

        public EnderecoViewModel? Endereco { get; set; }
    }
}