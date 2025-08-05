namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BaseNichoRequestDTO : BaseRequestDTO
    {
        public int Id { get; set; }
        public string? NomeNicho { get; set; }
    }
}
