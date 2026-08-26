using TSmartClinic.Shared.DTOs.Requests.Base;

namespace TSmartClinic.Shared.DTOs.Requests.Insert
{
    public class PacienteInsertRequestDTO : BasePacienteRequestDTO
    {
        public DateTime? DataCadastro { get; set; } = DateTime.Now;
        public List<PacienteEnderecoRequestDTO>? PacienteEnderecos { get; set; }
    }
}

