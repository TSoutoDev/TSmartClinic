namespace TSmartClinic.Core.Domain.Entities
{
    public class UsuarioClientePerfil
    {
        // Chave composta
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public int PerfilId { get; set; }
        public bool ClientePadrao { get; set; }

        #region Relacionamentos
        // Navegação
        public Usuario Usuario { get; set; } = null!;
        public Cliente Cliente { get; set; } = null!;
        public Perfil Perfil { get; set; } = null!;
        #endregion
    }
}
