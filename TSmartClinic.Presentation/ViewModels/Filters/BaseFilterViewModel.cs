namespace TSmartClinic.Presentation.ViewModels.Filters
{
    public class BaseFilterViewModel
    {
        public int? Id { get; set; }
        public string? Nome { get; set; } = null;
        public bool? Ativo { get; set; }
        public string OperadorLogico { get; set; } = "AND";
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
    }
}
