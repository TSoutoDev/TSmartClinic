using TSmartClinic.Data.Enums;

namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BaseTarefaRequestDTO : BaseRequestDTO
    {
        public string? Nome { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public PrioridadeTarefa? Prioridade { get; set; }
        public bool? FlagSituacao { get; set; }
        public int CategoriaId { get; set; }
    }
}
