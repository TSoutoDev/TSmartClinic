using TSmartClinic.Shared.DTOs.Requests.Base.PermissoesAcessoResponse;

namespace TSmartClinic.Shared.DTOs.Requests.Base
{
    public class BasePerfilRequestDTO : BaseRequestDTO
    {
        public string? NomePerfil { get; set; }
        public int? ValidadeDias { get; set; }
        public int? ErrosSenha { get; set; }
        public bool ResponsavelTecnico { get; set; }
        public bool Ativo { get; set; }
        public int? NichoId { get; set; }
        public int? ClienteId { get; set; }
        public List<BaseOperacaoPerfilRequestDTO>? OperacaoPerfis { get; set; }

        // A ÁRVORE para renderização
        public List<BasePermissoesAcessoRequestDTO.BaseModuloRequestDTO> Modulos { get; set; } = new();

        // As operações marcadas (é isso que a View envia/recebe)
        public List<int> SelectedOperacaoIds { get; set; } = new();

    }
}
