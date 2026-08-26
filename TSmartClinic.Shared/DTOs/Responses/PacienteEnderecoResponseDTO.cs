namespace TSmartClinic.Shared.DTOs.Responses
{
    public class PacienteEnderecoResponseDTO
    {
        public int PacienteId { get; set; }
        public int EnderecoId { get; set; }
        public string? Tipo { get; set; }

        public EnderecoResponseDTO? Endereco { get; set; }
    }
}