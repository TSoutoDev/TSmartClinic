using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Update
{
    public class PacienteUpdateRequestDTO : BasePacienteRequestDTO
    {
        public int? Id { get; set; }
        public List<PacienteEnderecoRequestDTO>? PacienteEnderecos { get; set; }
    }
}


