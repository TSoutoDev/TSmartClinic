namespace TSmartClinic.Presentation.Models
{
    public class UnidadeLoginViewModel
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string? NomeUnidade { get; set; }
        public int ClienteId { get; set; }
        public bool UnidadePadrao { get; set; }
    }
}