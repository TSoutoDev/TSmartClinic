namespace TSmartClinic.API.DTOs.Responses
{
    public class ConvenioResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? NomeConvenio { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
