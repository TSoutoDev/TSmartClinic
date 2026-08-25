using TSmartClinic.Core.Domain.Helpers;
using TSmartClinic.Core.Domain.Interfaces.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Convenio : Base, IEntidadePorCliente
    {
        public string? NomeConvenio { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int ClienteId { get; set; }



        #region Relacionamentos
        public Cliente? Cliente { get; set; }
        public ICollection<Paciente>? Pacientes { get; set; }
        #endregion

        public override void Atualizar(object obj)
        {
            Convenio convenio = obj as Convenio;

            this.NomeConvenio = convenio.NomeConvenio;
            this.Ativo = convenio.Ativo;
            this.ClienteId = convenio.ClienteId;

            this.RemoverEspacosEmBranco();
        }
    }
}
