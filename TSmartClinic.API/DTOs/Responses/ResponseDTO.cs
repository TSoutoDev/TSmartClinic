namespace TSmartClinic.API.DTOs.Responses
{
    public class ResponseDTO<TResponse>
    {
        public int PaginaAtual { get; set; }
        public int QuantidadeRegistros { get; set; }
        public List<TResponse> Itens { get; set; }
    }
}
