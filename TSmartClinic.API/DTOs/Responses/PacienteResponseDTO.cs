using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.API.DTOs.Responses
{
    public class PacienteResponseDTO : BaseResponseDTO
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
        public ConvenioResponseDTO? Convenio { get; set; }
    }
}
