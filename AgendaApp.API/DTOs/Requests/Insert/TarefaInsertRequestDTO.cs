using AgendaApp.API.DTOs.Requests.Base;
using AgendaApp.Data.Enums;

namespace AgendaApp.API.DTOs.Requests.Insert
{
    public class TarefaInsertRequestDTO : BaseTarefaRequestDTO
    {
        public DateTime? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
    }
}
