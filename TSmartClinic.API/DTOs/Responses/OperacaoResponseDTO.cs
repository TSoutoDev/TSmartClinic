namespace TSmartClinic.API.DTOs.Responses
{
    public class OperacaoResponseDTO: BaseResponseDTO
    {
        public int? Id { get; set; }
        public string? NomeOperacao { get; set; }
        public string? Descricao { get; set; }
        public int? FuncionalidadeId { get; set; }

    }
}
