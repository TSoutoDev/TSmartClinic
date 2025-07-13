using AgendaApp.Data.Entities;
using AgendaApp.Data.Enums;

namespace AgendaApp.API.DTOs.Responses
{
    public class TarefaResponseDTO : BaseResponseDTO
    {
        public string? Nome { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public PrioridadeTarefa? Prioridade { get; set; }
        public bool? FlagSituacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        public int CategoriaId { get; set; }
    }
}
