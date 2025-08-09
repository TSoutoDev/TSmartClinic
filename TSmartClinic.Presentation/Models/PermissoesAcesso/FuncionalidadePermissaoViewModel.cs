namespace TSmartClinic.Presentation.Models.PermissoesAcesso
{
    public class FuncionalidadePermissaoViewModel
    {
        public int Id { get; set; }
        public string? NomeFuncionalidade { get; set; }
        public List<OperacaoPermissaoViewModel>? Operacoes { get; set; }
    }
}
