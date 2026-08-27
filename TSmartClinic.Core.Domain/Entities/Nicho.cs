using TSmartClinic.Core.Domain.Interfaces.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Nicho : Base, IEntidadeComPublicId
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string? NomeNicho { get; set; }
        public bool? Ativo { get; set; }

        #region Relacionamentos
        public List<Perfil>? Perfis { get; set; }
        public ICollection<Cliente>? Clinicas { get; set; }
        #endregion


        public override void Atualizar(Object obj)
        {
            Nicho nicho = obj as Nicho;

            this.NomeNicho = nicho?.NomeNicho;
        }
    }

}
