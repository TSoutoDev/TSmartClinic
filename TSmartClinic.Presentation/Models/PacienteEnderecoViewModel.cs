namespace TSmartClinic.Presentation.Models
{
    public class PacienteEnderecoViewModel
    {
        public int? PacienteId { get; set; }
        public int? EnderecoId { get; set; }
        public string? Tipo { get; set; }

        public EnderecoViewModel? Endereco { get; set; }
    }
}
