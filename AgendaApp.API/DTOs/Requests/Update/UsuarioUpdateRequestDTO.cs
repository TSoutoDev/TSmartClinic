using AgendaApp.API.DTOs.Requests.Base;

namespace AgendaApp.API.DTOs.Requests.Update
{
    public class UsuarioUpdateRequestDTO : BaseUsuarioRequestDTO
    {
        public int Id { get; set; }
        public string? LoginAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }     
    }
}
