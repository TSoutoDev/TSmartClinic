using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Entities
{
    public class Categoria : Base
    {
        #region Propriedades
        public string? Descricao { get; set; }
        public bool? FlagSituacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string? UsuarioCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        #endregion

        #region Relacionamentos
        public List<Tarefa>? Tarefas { get; set; }
        #endregion
    }
}
