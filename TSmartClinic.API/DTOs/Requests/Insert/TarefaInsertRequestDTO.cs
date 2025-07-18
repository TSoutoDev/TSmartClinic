using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Insert
{
    public class TarefaInsertRequestDTO : BaseTarefaRequestDTO
    {
        public DateTime? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
    }
}
