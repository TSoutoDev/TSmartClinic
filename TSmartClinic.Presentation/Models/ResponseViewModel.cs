using Azure;

namespace TSmartClinic.Presentation.Models
{
    public class ResponseViewModel<TResponse> where TResponse : BaseViewModel
    {
        public bool Sucesso { get; set; }
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
        public int Pagina { get; set; }
        public int ItensPorPagina { get; set; }
        public int QuantidadeRegistros { get; set; }
        public List<TResponse> Itens { get; set; } = new List<TResponse>();
    }
}
