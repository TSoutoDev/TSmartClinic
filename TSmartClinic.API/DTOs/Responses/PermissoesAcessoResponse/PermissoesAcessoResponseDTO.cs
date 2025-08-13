namespace TSmartClinic.API.DTOs.Responses.PermissoesAcessoResponse
{
    public class PermissoesAcessoResponseDTO : BaseResponseDTO
    {
        public class OperacaoViewModel
        {
            public int Id { get; set; }
            public string NomeOperacao { get; set; } = "";
            public int FuncionalidadeId { get; set; }
            public bool Selecionada { get; set; } // opcional para marcar no GET
        }

        public class FuncionalidadeViewModel
        {
            public int Id { get; set; }
            public string NomeFuncionalidade { get; set; } = "";
            public int ModuloId { get; set; }
            public List<OperacaoViewModel> Operacoes { get; set; } = new();
        }

        public class ModuloViewModel
        {
            public int Id { get; set; }
            public string NomeModulo { get; set; } = "";
            public List<FuncionalidadeViewModel> Funcionalidades { get; set; } = new();
        }
    }
}
