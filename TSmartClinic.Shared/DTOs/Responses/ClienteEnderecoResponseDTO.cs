using TSmartClinic.Shared.DTOs.Responses;

public class ClienteEnderecoResponseDTO
{
    public int? ClienteId { get; set; }
    public int? EnderecoId { get; set; }
    public string? Tipo { get; set; }

    public EnderecoResponseDTO? Endereco { get; set; }
}