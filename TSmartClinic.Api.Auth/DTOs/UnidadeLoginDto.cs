namespace TSmartClinic.Api.Auth.DTOs
{
    public class UnidadeLoginDto
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string? NomeUnidade { get; set; }
        public int ClienteId { get; set; }
        public bool UnidadePadrao { get; set; }
    }
}