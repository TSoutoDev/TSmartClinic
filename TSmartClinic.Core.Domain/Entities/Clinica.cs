using TSmartClinic.Data.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Clinica : Base
    {
        public string? NomeClinica { get; set; }
        public string? RazaoSocial { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? NichoId { get; set; }

        #region Relacionamentos
        public Nicho? Nicho { get; set; }
        #endregion
    }
}
