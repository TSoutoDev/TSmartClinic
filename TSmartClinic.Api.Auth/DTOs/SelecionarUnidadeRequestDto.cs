namespace TSmartClinic.Api.Auth.DTOs
{
    public class SelecionarUnidadeRequestDto
    {
        public string TokenSelecaoUnidade { get; set; } = string.Empty;
        public int UnidadeId { get; set; }
        public bool DefinirComoPadrao { get; set; }
    }
}