namespace TSmartClinic.Presentation.Models.PermissoesAcesso
{
    public class ModuloPermissaoViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<FuncionalidadePermissaoViewModel>? Funcionalidades { get; set; }
    }
}
