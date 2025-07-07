namespace AgendaApp.Core.Domain.Helpers.FilterHelper
{
    public class BaseFiltro
    {
        public virtual int? Id { get; set; } = null!;
        public virtual string? Nome { get; set; } = null!;
        public virtual bool? Ativo { get; set; } = null!;

        public string OperadorLogico { get; set; } = "AND";

        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
    }
}
