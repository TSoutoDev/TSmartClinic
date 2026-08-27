using TSmartClinic.Core.Domain.Helpers;
using TSmartClinic.Core.Domain.Interfaces.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Paciente : Base, IEntidadePorCliente, IEntidadeComPublicId
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string? NomePaciente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Observacoes { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; } = DateTime.UtcNow;
        public int? ConvenioId { get; set; }
        public byte[]? Foto { get; set; }
        public int ClienteId { get; set; }

        #region Relacionamentos
        public Convenio? Convenio { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<PacienteEndereco>? PacienteEnderecos { get; set; }
        #endregion

        public override void Atualizar(object obj)
        {
            var paciente = obj as Paciente;

            if (paciente == null)
                return;

            NomePaciente = paciente.NomePaciente;
            DataNascimento = paciente.DataNascimento;
            CPF = paciente.CPF;
            Telefone = paciente.Telefone;
            Email = paciente.Email;
            Observacoes = paciente.Observacoes;
            Ativo = paciente.Ativo;
            ConvenioId = paciente.ConvenioId;
            Foto = paciente.Foto;

            this.RemoverEspacosEmBranco();
        }
    }
}