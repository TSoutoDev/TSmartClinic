using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.API.DTOs.Requests.Base
{
    public class BasePerfilRequestDTO : BaseRequestDTO
    {
        public string? NomePerfil { get; set; }
        public int? ValidadeDias { get; set; }
        public int? ErrosSenha { get; set; }
        public bool? ResponsavelTecnico { get; set; }
        public bool? Ativo { get; set; }
        public int? NichoId { get; set; }
        public int? ClienteId { get; set; }
    }
}
