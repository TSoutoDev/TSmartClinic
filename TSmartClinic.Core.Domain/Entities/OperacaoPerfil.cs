using TSmartClinic.Core.Domain.Helpers;

namespace TSmartClinic.Core.Domain.Entities
{
    public class OperacaoPerfil : Base
    {
        public int PerfilId { get; set; }
        public int OperacaoId { get; set; }

        public Operacao? Operacao { get; set; }
        public Perfil? Perfil { get; set; }

        public override void Atualizar(Object obj)
        {
            OperacaoPerfil operacaoPerfil = obj as OperacaoPerfil;

            this.PerfilId = operacaoPerfil.PerfilId;
            this.OperacaoId = operacaoPerfil.OperacaoId;
        }
    }
}