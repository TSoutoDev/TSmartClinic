using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Update
{
    public class PacienteUpdateRequestDTO : BasePacienteDTO
    {
        public int? Id { get; set; }
    }
}
