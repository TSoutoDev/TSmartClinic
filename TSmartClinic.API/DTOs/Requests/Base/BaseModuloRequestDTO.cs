namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BaseModuloRequestDTO : BaseRequestDTO
    {
        public string? NomeModulo { get; set; }
        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }
    }
}
