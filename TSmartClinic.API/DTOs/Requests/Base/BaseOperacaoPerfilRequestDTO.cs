using TSmartClinic.API.DTOs.Responses;

namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BaseOperacaoPerfilRequestDTO : BaseResponseDTO
    {
        public int PerfilId { get; set; }
        public int OperacaoId { get; set; }
    }
}
