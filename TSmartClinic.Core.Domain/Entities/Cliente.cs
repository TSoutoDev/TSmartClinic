using TSmartClinic.Data.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Cliente : Base
    {
        public string? NomeFantasia { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Cnpj{ get; set; }
        public string? Telefone { get; set; }
        public string? EmailContato { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? NichoId { get; set; }

        #region Relacionamentos
        public Nicho? Nicho { get; set; }
        public ICollection<Perfil> Perfis { get; set; } = new List<Perfil>();        // Relacionamento 1:N com Perfil
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();  
      //  public ICollection<Clinica> Clinicas { get; set; } = new List<Clinica>();  
        #endregion
    }
}
