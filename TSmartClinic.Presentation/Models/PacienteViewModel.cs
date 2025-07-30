using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Presentation.Models
{
    public class PacienteViewModel : BaseViewModel
    {
        public string? NomePaciente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Observacoes { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? ConvenioId { get; set; }


        #region Relacionamentos
        public Convenio? Convenio { get; set; }
        #endregion
    }
}
