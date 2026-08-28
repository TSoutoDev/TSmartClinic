namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseClienteRequestDTO : BaseRequestDTO
    {
        public string? NomeCliente { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? EmailContato { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public byte[]? Logo { get; set; }
        public int? NichoId { get; set; }
        public List<BaseClienteEnderecoRequestDTO>? ClienteEnderecos { get; set; }
    }
}
