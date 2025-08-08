namespace TSmartClinic.Core.Domain.Entities
{
    public class UsuarioClientePerfil : Base
    {
        public int ClienteId { get; set; }
        public int PerfilId { get; set; }

        public bool ClientePadrao { get; set; }

        #region Relacionamentos
        public Usuario? Usuario { get; set; }
        public Cliente? Clinica { get; set; }
        public Perfil? Perfil { get; set; }
        #endregion
    }
}
