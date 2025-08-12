using static TSmartClinic.Presentation.Models.PermissoesAcesso.PermissoesViewModel;

namespace TSmartClinic.Presentation.Models
{
    public class PerfilViewModel : BaseViewModel
    {
        public string? NomePerfil { get; set; }
        public int? ValidadeDias { get; set; }
        public int? ErrosSenha { get; set; }
        public bool ResponsavelTecnico { get; set; }
        public bool Ativo { get; set; } = true;
        public int? NichoId { get; set; }
        public int? ClienteId { get; set; }
        public NichoViewModel? Nicho {  get; set; }
        public List<ClienteViewModel>? ListClientes { get; set; }


        public string ResponsavelTecnicoFormatado
        {
            get
            {
                return ResponsavelTecnico switch
                {
                    true => "SIM",
                    false => "NÃO"
                };
            }
        }
      
        //public List<ModuloViewModel> Modulos { get; set; } = new();               // exibição da árvore
        //public List<int> SelectedOperacaoIds { get; set; } = new();         // recebe os switches no POST

        // A ÁRVORE para renderização
        public List<ModuloViewModel> Modulos { get; set; } = new();

        // As operações marcadas (é isso que a View envia/recebe)
        public List<int> SelectedOperacaoIds { get; set; } = new();

    }
}
