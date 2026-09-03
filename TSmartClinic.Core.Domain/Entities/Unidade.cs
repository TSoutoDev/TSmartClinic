using TSmartClinic.Core.Domain.Helpers;
using TSmartClinic.Core.Domain.Interfaces.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Unidade : Base, IEntidadePorCliente, IEntidadeComPublicId
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public int ClienteId { get; set; }
        public string? NomeUnidade { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public bool UnidadePrincipal { get; set; }
        public DateTime? DataCadastro { get; set; }

        #region Relacionamentos
        public Cliente? Cliente { get; set; }
        public ICollection<UsuarioUnidadePerfil>? UsuariosUnidadePerfil { get; set; }
        public ICollection<UnidadeEndereco>? Enderecos { get; set; }

        #endregion

        public override void Atualizar(object obj)
        {
            if (obj is not Unidade unidade)
                return;

            ClienteId = unidade.ClienteId;
            NomeUnidade = unidade.NomeUnidade;
            Cnpj = unidade.Cnpj;
            Telefone = unidade.Telefone;
            Email = unidade.Email;
            Ativo = unidade.Ativo;
            UnidadePrincipal = unidade.UnidadePrincipal;

            this.RemoverEspacosEmBranco();
        }
    }
}