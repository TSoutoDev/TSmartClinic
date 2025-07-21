namespace TSmartClinic.Presentation.ViewModels.Filters
{
    public class BaseFilterViewModel
    {
        public string? Nome { get; set; } = null;
        public bool? Ativo { get; set; }
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
    }
}
