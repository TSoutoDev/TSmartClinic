using AgendaApp.API.DTOs.Requests.Base;

namespace AgendaApp.API.DTOs.Requests.Insert
{
    public class UsuarioInsertRequestDTO : BaseUsuarioRequestDTO
    {
        public string? LoginInclusao { get; set; }
        public DateTime? DataInclusao { get; set; }
    }
}
