using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Update
{
    public class TarefaUpdateRequestDTO : BaseTarefaRequestDTO
    {
        public DateTime? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
    }
}
