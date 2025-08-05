using TSmartClinic.API.DTOs.Requests.Base;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.API.DTOs.Requests.Insert
{
    public class PacienteInsertRequestDTO : BasePacienteRequestDTO
    {
        public DateTime? DataCadastro { get; set; } = DateTime.Now;

    }
}
