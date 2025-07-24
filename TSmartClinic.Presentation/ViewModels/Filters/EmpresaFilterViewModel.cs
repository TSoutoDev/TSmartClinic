namespace TSmartClinic.Presentation.ViewModels.Filters
{
    public class EmpresaFilterViewModel : BaseFilterViewModel
    {
        public string? Codigo { get; set; }
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
    }
}
