namespace TSmartClinic.Shared.DTOs.Responses
{
    public class ClienteResponseDTO : BaseResponseDTO
    {
        public int? Id { get; set; }
        public Guid PublicId { get; set; }
        public string? NomeCliente { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? EmailContato { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public byte[]? Logo { get; set; }
        public int? NichoId { get; set; }
        public List<ClienteEnderecoResponseDTO>? ClienteEnderecos { get; set; }
    }
}


