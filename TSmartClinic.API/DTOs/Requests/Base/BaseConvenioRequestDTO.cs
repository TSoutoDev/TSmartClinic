namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BaseConvenioRequestDTO : BaseRequestDTO
    {
        public string? NomeConvenio { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
