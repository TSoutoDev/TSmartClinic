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
        public List<OperacaoPerfil> OperacaoPerfis { get; set; } = new(); 
        public Cliente? Cliente { get; set; }

        public List<UsuarioClientePerfil> UsuarioClientePerfil { get; set; } = new();
        #endregion

        public override void Atualizar(object obj)
        {
            Perfil perfil = obj as Perfil;

            // escalares
            NomePerfil = perfil.NomePerfil;
            ValidadeDias = perfil.ValidadeDias;
            ErrosSenha = perfil.ErrosSenha;
            ResponsavelTecnico = perfil.ResponsavelTecnico;
            Ativo = perfil.Ativo;
            NichoId = perfil.NichoId;

            this.OperacaoPerfis = perfil.OperacaoPerfis
           .Select(e => new OperacaoPerfil
           {
               PerfilId = e.PerfilId,
               OperacaoId = e.OperacaoId,
           })
           .ToList();

            this.RemoverEspacosEmBranco();
        }

    }
}
