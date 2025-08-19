using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class TarefaInsertRequestDTO : BaseTarefaRequestDTO
    {
        public DateTime? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
    }
}

