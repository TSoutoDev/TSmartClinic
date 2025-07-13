using AgendaApp.Core.Domain.Entities;
using AgendaApp.Data.Enums;

namespace AgendaApp.Data.Entities
{
    public class Tarefa : Base
    {
        #region Propriedades
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
        #endregion

        #region Relacionamentos
        public Categoria? Categoria { get; set; }
        #endregion
    }
}
