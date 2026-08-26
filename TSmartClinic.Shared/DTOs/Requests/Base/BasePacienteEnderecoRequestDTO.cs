namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class PacienteEnderecoRequestDTO
    {
        public string? Tipo { get; set; }
        public BaseEnderecoRequestDTO? Endereco { get; set; }
    }
}
