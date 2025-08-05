using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Update
{
    public class PacienteUpdateRequestDTO : BasePacienteRequestDTO
    {
        public int? Id { get; set; }
    }
}
