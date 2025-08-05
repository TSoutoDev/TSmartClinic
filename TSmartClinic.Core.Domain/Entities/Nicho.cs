namespace TSmartClinic.Core.Domain.Entities
{
    public class Nicho : Base
    {
        public string? NomeNicho { get; set; }

        #region Relacionamentos
        public List<Perfil>? Perfis { get; set; }
        public ICollection<Cliente>? Clinicas { get; set; }
        #endregion
    }
}
