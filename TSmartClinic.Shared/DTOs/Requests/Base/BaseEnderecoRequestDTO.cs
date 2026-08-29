namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BaseEnderecoRequestDTO
    {
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }

        public int? EstadoId { get; set; }
        public int? MunicipioId { get; set; }
    }
}
