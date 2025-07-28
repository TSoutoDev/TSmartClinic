namespace TSmartClinic.Presentation.Models
{

    public class DashboardViewModel
    {
        public int TotalAcoes { get; set; }
        public int AcoesAtivas { get; set; }
        public int AcoesInativas { get; set; }
        public List<string> UltimasAcoes { get; set; }

        // Estas são as propriedades que estavam faltando:
        public List<int> AcoesPorMes { get; set; }      // Ex: [2, 3, 5, 4, 1]
        public List<string> Meses { get; set; }         // Ex: ["Jan", "Fev", "Mar", ...]
    }
}



