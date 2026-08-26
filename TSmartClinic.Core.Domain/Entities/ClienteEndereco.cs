namespace TSmartClinic.Core.Domain.Entities
{
    public class ClienteEndereco
    {
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        public string? Tipo { get; set; }

        #region Relacionamentos
        public Cliente? Cliente { get; set; }
        public Endereco? Endereco { get; set; }
        #endregion
    }
}