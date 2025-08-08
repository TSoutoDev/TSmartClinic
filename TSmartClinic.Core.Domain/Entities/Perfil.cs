using TSmartClinic.Core.Domain.Helpers;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Perfil : Base
    {
        public string? NomePerfil { get; set; }
        public int? ValidadeDias { get; set; }
        public int? ErrosSenha { get; set; }
        public bool? ResponsavelTecnico { get; set; }
        public bool? Ativo { get; set; }
        public int? NichoId { get; set; }
        public int? ClienteId { get; set; } 

        #region Relacionamentos
        public Nicho? Nicho { get; set; }
        public OperacaoPerfil? OperacaoPerfis { get; set; }
        public Cliente? Cliente { get; set; } = null!; // Navegação para Cliente
        #endregion


        public override void Atualizar(Object obj)
        {
            Perfil perfil = obj as Perfil;

            this.NomePerfil = perfil?.NomePerfil;
            this.ValidadeDias = perfil?.ValidadeDias;
            this.ErrosSenha = perfil?.ErrosSenha;
            this.ResponsavelTecnico = perfil?.ResponsavelTecnico;
            this.Cliente = perfil.Cliente;
            this.Ativo = perfil.Ativo;
            this.NichoId = perfil.NichoId;

            this.RemoverEspacosEmBranco();
        }
    }
}
