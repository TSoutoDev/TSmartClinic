namespace TSmartClinic.Shared.DTOs.Responses
{
    public class NichoResponseDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string? NomeNicho { get; set; }
        public bool? Ativo { get; set; }
    }
}
