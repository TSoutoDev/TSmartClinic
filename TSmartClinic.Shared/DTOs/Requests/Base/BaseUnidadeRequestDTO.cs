namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseUnidadeRequestDTO : BaseRequestDTO
    {
        public int ClienteId { get; set; }
        public string? NomeUnidade { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; } = true;
        public bool UnidadePrincipal { get; set; }
    }
}