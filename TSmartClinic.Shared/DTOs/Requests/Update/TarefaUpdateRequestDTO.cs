using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Update
{
    public class TarefaUpdateRequestDTO : BaseTarefaRequestDTO
    {
        public DateTime? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
    }
}


