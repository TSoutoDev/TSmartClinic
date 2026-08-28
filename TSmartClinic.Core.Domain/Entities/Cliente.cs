using TSmartClinic.Core.Domain.Helpers;
using TSmartClinic.Core.Domain.Interfaces.Entities;
using TSmartClinic.Data.Entities;

namespace TSmartClinic.Core.Domain.Entities
{
    public class Cliente : Base, IEntidadeComPublicId
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public string? NomeCliente { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? EmailContato { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public byte[]? Logo { get; set; }
        public int? NichoId { get; set; }

        #region Relacionamentos

        public Nicho? Nicho { get; set; }
        public ICollection<Perfil> Perfis { get; set; } = new List<Perfil>();
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<UsuarioClientePerfil> UsuarioClientePerfil { get; set; } = new();
        public ICollection<ClienteEndereco>? ClienteEndereco { get; set; }

        #endregion

        public override void Atualizar(object obj)
        {
            Cliente cliente = obj as Cliente;

            NomeCliente = cliente.NomeCliente;
            RazaoSocial = cliente.RazaoSocial;
            Cnpj = cliente.Cnpj;
            Telefone = cliente.Telefone;
            EmailContato = cliente.EmailContato;
            Ativo = cliente.Ativo;
            DataCadastro = cliente.DataCadastro;
            NichoId = cliente.NichoId;

            if (cliente.Logo != null && cliente.Logo.Length > 0)
            {
                Logo = cliente.Logo;
            }

            this.RemoverEspacosEmBranco();
        }
    }
}