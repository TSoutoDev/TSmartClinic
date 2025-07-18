namespace TSmartClinic.Core.Domain.Entities
{
    public class Funcionalidade : Base
    {
        #region Propriedades
        public string? NomeFuncionalidade { get; set; }
        public string? Descricao { get; set; }
        public int? ModuloId { get; set; }
      
        #endregion

        #region Relacionamentos
        public Modulo? Modulo { get; set; }
        public ICollection<Operacao>? Operacoes { get; set; }
        #endregion

    }
}
