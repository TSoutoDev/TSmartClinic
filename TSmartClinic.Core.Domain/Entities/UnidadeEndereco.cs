namespace TSmartClinic.Core.Domain.Entities
{
    public class UnidadeEndereco
    {
        public int UnidadeId { get; set; }
        public int EnderecoId { get; set; }
        public string? Tipo { get; set; }

        #region Relacionamentos
        public Unidade? Unidade { get; set; }
        public Endereco? Endereco { get; set; }
        #endregion
    }
}