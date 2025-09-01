namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseNichoRequestDTO : BaseRequestDTO
    {
        public int Id { get; set; }
        public string? NomeNicho { get; set; }
        public bool? Ativo { get; set; }
    }
}


