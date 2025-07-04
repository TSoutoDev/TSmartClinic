using AgendaApp.Data.Enums;

namespace AgendaApp.Data.Entities
{
    public class Tarefa
    {
        #region Propriedades
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public PrioridadeTarefa? Prioridade { get; set; }
        public bool? FlagSituacao { get; set; }
        public string? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public string? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        public Guid CategoriaId { get; set; }
        #endregion

        #region Relacionamentos
        public Categoria? Categoria { get; set; }
        #endregion
    }
}
