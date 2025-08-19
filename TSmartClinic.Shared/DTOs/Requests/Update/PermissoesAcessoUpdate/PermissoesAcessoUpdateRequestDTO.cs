using TSmartClinic.Shared.DTOs.Requests.Base.PermissoesAcessoResponse;

namespace TSmartClinic.Shared.DTOs.Requests.Update.PermissoesAcessoResponse
{
    public class PermissoesAcessoUpdateRequestDTO : BasePermissoesAcessoRequestDTO
    {
        public class OperacaoUpdateRequestDto
        {
            public int Id { get; set; }
        }

        public class FuncionalidadeUpdateRequestDTO
        {
            public int Id { get; set; }
        }

        public class ModuloUpdateRequestDTO
        {
            public int Id { get; set; }
        }
    }
}
