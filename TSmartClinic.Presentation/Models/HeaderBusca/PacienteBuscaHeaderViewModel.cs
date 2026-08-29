namespace TSmartClinic.Presentation.Models
{
    public class PacienteBuscaHeaderViewModel
    {
        public Guid PublicId { get; set; }
        public string? NomePaciente { get; set; }
        public string? CPF { get; set; }
        public int ClienteId { get; set; }
        public string? NomeClinica { get; set; }
    }
}