namespace TSmartClinic.Core.Domain.Entities
{
    public class Convenio : Base
    {
        public string? NomeConvenio { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }

        #region Relacionamentos
        public ICollection<Paciente>? Pacientes { get; set; }
        #endregion
    }
}
