using TSmartClinic.API.DTOs.Requests.Base;

namespace TSmartClinic.API.DTOs.Requests.Update
{
    public class UsuarioUpdateRequestDTO : BaseUsuarioRequestDTO
    {
        public int Id { get; set; }
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }     
    }
}
