using TSmartClinic.Core.Domain.Helpers;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Paciente : Base
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


        public override void Atualizar(Object obj)
        {
            Paciente paciente = obj as Paciente;

            this.NomePaciente = paciente.NomePaciente;
            this.DataNascimento = paciente.DataNascimento;
            this.CPF = paciente.CPF;
            this.Telefone = paciente.Telefone;
            this.Email = paciente.Email;
            this.Observacoes = paciente.Observacoes;         
            this.Ativo = paciente.Ativo;
            this.ConvenioId = paciente.ConvenioId;
             
            this.RemoverEspacosEmBranco();
        }
    }
}
