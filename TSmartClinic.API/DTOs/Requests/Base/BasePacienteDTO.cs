using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BasePacienteDTO : BaseRequestDTO
    {
        public string? NomePaciente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Observacoes { get; set; }
        public bool? Ativo { get; set; }
        public int? ConvenioId { get; set; }
        public byte[]? Foto { get; set; }

    }
}
