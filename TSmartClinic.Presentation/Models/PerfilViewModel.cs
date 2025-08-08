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

    }
}
