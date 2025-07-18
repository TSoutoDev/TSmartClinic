namespace TSmartClinic.Core.Domain.Entities
{
    public class Modulo : Base
    {
        public string? NomeModulo { get; set; }
        public string? Descricao { get; set; }
        public bool? Ativo { get; set; }
    }
}
