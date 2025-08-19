namespace TSmartClinic.Shared.DTOs.Requests.Base.PermissoesAcessoResponse
{
    public class BasePermissoesAcessoRequestDTO : BaseRequestDTO
    {
        public class BaseOperacaoRequestDto
        {
            public int Id { get; set; }
            public string NomeOperacao { get; set; } = "";
            public int FuncionalidadeId { get; set; }
            public bool Selecionada { get; set; } // opcional para marcar no GET
        }

        public class BaseFuncionalidadeRequestDTO
        {
            public int Id { get; set; }
            public string NomeFuncionalidade { get; set; } = "";
            public int ModuloId { get; set; }
            public List<BaseOperacaoRequestDto> Operacoes { get; set; } = new();
        }

        public class BaseModuloRequestDTO
        {
            public int Id { get; set; }
            public string NomeModulo { get; set; } = "";
            public List<BaseFuncionalidadeRequestDTO> Funcionalidades { get; set; } = new();
        }
    }
}
