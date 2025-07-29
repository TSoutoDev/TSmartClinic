namespace TSmartClinic.Core.Domain.Entities
{
    public class UsuarioClinicaPerfil : Base
    {
        public int ClinicaId { get; set; }
        public int PerfilId { get; set; }

        public bool ClinicaPadrao { get; set; }

        #region Relacionamentos
        public Usuario? Usuario { get; set; }
        public Clinica? Clinica { get; set; }
        public Perfil? Perfil { get; set; }
        #endregion
    }
}
