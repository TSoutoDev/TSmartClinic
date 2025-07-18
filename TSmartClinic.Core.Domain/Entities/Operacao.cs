namespace TSmartClinic.Core.Domain.Entities
{
    public class Operacao : Base
    {
        public string? NomeOperacao { get; set; }
        public string? Descricao { get; set; }
        public int? FuncionalidadeId { get; set; }

        #region Relacionamentos
        public Funcionalidade? Funcionalidade { get; set; }
        public List<OperacaoPerfil>? OperacaoPerfis { get; set; }
        #endregion
    }
}
