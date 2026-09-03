namespace TSmartClinic.Presentation.Models
{
    public class SelecionarUnidadeViewModel
    {
        public int? UnidadeId { get; set; }
        public bool DefinirComoPadrao { get; set; }
        public List<UnidadeLoginViewModel> Unidades { get; set; } = new();
    }
}