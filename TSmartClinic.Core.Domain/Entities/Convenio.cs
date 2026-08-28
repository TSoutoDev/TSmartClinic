using TSmartClinic.Core.Domain.Helpers;
using TSmartClinic.Core.Domain.Interfaces.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Convenio : Base, IEntidadePorCliente, IEntidadeComPublicId
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();

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
            if (obj is not Convenio convenio)
                return;

            NomeConvenio = convenio.NomeConvenio;
            CNPJ = convenio.CNPJ;
            Telefone = convenio.Telefone;
            Email = convenio.Email;
            Ativo = convenio.Ativo;
            ClienteId = convenio.ClienteId;

            this.RemoverEspacosEmBranco();
        }
    }
}