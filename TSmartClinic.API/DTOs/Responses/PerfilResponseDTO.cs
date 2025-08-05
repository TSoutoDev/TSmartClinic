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
        public bool? Ativo { get; set; }
        public NichoResponseDTO? Nicho { get; set; }
        public int? NichoId { get; set; }
        public int? ClienteId { get; set; }


    }
}
