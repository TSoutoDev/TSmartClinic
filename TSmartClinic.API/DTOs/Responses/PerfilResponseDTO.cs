using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.DTOs.Responses
{
    public class PerfilResponseDTO : BaseResponseDTO
    {
        public int? Id { get; set; }
        public string? NomePerfil { get; set; }
        public int? ValidadeDias { get; set; }
        public int? ErrosSenha { get; set; }
        public bool? ResponsavelTecnico { get; set; }
        public bool? Cliente { get; set; }
        public bool? Ativo { get; set; }
        public int? NichoId { get; set; }

        public List<NichoResponseDTO>? ListaNichos{get; set;}
    }
}
