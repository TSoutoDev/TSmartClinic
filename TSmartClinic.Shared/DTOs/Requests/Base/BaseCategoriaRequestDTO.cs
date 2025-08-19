namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseCategoriaRequestDTO : BaseRequestDTO
    {
        public string? Descricao { get; set; }
        public bool? FlagSituacao { get; set; }
    }
}


