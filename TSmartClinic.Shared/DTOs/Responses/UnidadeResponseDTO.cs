namespace TSmartClinic.Shared.DTOs.Responses
{
    public class UnidadeResponseDTO : BaseResponseDTO
    {
        public Guid PublicId { get; set; }
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? NomeUnidade { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public bool UnidadePrincipal { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}