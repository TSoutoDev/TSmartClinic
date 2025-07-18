namespace TSmartClinic.Core.Domain.Entities
{
    public class Perfil : Base
    {
        public string? NomePerfil { get; set; }
        public int? ValidadeDias { get; set; }
        public int? ErrosSenha { get; set; }
        public bool? Administrador { get; set; }
        public bool? ResponsavelTecnico { get; set; }
        public bool? Cliente { get; set; }
        public bool? Ativo { get; set; }
        public int? NichoId { get; set; }

        #region Relacionamentos
        public Nicho? Nicho { get; set; }
        public OperacaoPerfil? OperacaoPerfis { get; set; }
        #endregion
    }
}
