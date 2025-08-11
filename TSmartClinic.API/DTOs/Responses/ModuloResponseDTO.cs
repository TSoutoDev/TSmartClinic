namespace TSmartClinic.API.DTOs.Responses
{
    public class ModuloResponseDTO : BaseResponseDTO
    {
        public int? Id { get; set; }
        public string? NomeModulo { get; set; }
        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }

        public List<FuncionalidadeResponseDTO>? Funcionalidades { get; set; }
     
    }
}
