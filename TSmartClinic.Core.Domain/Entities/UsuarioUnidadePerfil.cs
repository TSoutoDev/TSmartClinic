namespace TSmartClinic.Core.Domain.Entities
{
    public class UsuarioUnidadePerfil
    {
        public int UsuarioId { get; set; }
        public int UnidadeId { get; set; }
        public int PerfilId { get; set; }
        public bool UnidadePadrao { get; set; }

        #region Relacionamentos
        public Usuario Usuario { get; set; } = null!;
        public Unidade Unidade { get; set; } = null!;
        public Perfil Perfil { get; set; } = null!;
        #endregion
    }
}