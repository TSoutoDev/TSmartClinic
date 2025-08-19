namespace TSmartClinic.Shared.DTOs.Responses.PermissoesAcessoRersponse
{
    public class PermissoesAcessoResponseDTO : BaseResponseDTO
    {
        public class OperacaoResponseDTO
        {
            public int Id { get; set; }
            public string NomeOperacao { get; set; } = "";
            public int FuncionalidadeId { get; set; }
            public bool Selecionada { get; set; } // opcional para marcar no GET
        }

        public class FuncionalidadeResponseDTO
        {
            public int Id { get; set; }
            public string NomeFuncionalidade { get; set; } = "";
            public int ModuloId { get; set; }
            public List<OperacaoResponseDTO> Operacoes { get; set; } = new();
        }

        public class ModuloResponseDTO
        {
            public int Id { get; set; }
            public string NomeModulo { get; set; } = "";//------
            public List<FuncionalidadeResponseDTO> Funcionalidades { get; set; } = new();
        }
    }
}
