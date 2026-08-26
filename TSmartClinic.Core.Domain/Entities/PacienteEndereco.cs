namespace TSmartClinic.Core.Domain.Entities
{
    public class PacienteEndereco
    {
        public int PacienteId { get; set; }
        public int EnderecoId { get; set; }
        public string? Tipo { get; set; }

        #region Relacionamentos
        public Paciente? Paciente { get; set; }
        public Endereco? Endereco { get; set; }
        #endregion
    }
}