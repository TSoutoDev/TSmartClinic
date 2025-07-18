namespace TSmartClinic.Core.Domain.Entities
{
    public class OperacaoPerfil 
    {
        public int PerfilId { get; set; }
        public int OperacaoId { get; set; }

        #region Relacionamentos
        public Operacao? Operacao { get; set; }
        public Perfil? Perfil { get; set; }
        #endregion
    }
}
