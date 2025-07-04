namespace AgendaApp.Data.Entities
{
    public class Categoria
    {
        #region Propriedades
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
        public bool? FlagSituacao { get; set; }
        public string? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public string? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        #endregion

        #region Relacionamentos
        public List<Tarefa>? Tarefas { get; set; }
        #endregion
    }
}
