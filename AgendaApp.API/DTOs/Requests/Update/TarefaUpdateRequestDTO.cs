using AgendaApp.API.DTOs.Requests.Base;

namespace AgendaApp.API.DTOs.Requests.Update
{
    public class TarefaUpdateRequestDTO : BaseTarefaRequestDTO
    {
        public DateTime? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
    }
}
