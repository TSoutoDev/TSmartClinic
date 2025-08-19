namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BasePacienteRequestDTO : BaseRequestDTO
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

