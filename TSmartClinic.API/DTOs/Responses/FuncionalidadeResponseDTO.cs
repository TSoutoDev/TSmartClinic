namespace TSmartClinic.API.DTOs.Responses
{
    public class FuncionalidadeResponseDTO : BaseResponseDTO
    {
        public int? Id { get; set; }
        public string? NomeFuncionalidade { get; set; }
        public string? Descricao { get; set; }
        public int? ModuloId { get; set; }
    }
}
