using AgendaApp.API.DTOs.Requests.Base;

namespace AgendaApp.API.DTOs.Requests.Update
{
    public class CategoriaUpdateRequestDTO : BaseCategoriaRequestDTO
    {
        public int Id { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.UtcNow;
        public string? UsuarioAlteracao { get; set; }
    }
}
