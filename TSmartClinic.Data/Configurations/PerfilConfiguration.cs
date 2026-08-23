using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            // schema e nome da tabela
            builder.ToTable("Perfil", "dbo");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd() // diga ao EF que o valor é gerado;
                .UseIdentityByDefaultColumn(); // mapeia identity do Postgres
            builder.Property(c => c.NomePerfil).HasColumnName("NomePerfil").HasMaxLength(100).IsRequired();
            builder.Property(c => c.ValidadeDias).HasColumnName("ValidadeDias");
            builder.Property(c => c.ErrosSenha).HasColumnName("ErrosSenha");
            builder.Property(c => c.ResponsavelTecnico).HasColumnName("ResponsavelTecnico");
            builder.Property(c => c.Ativo).HasColumnName("Ativo");
            builder.Property(c => c.NichoId).HasColumnName("NichoId").IsRequired();
            builder.Property(c => c.ClienteId).HasColumnName("ClienteId").IsRequired();

            // PK gerada pelo banco (IDENTITY)  
            //mapeamento do relacionamento (1pN)
            builder.HasOne(f => f.Nicho)
            .WithMany(o => o.Perfis)
            .HasForeignKey(f => f.NichoId);

            // Relacionamento com Cliente (novo)
            builder.HasOne(f => f.Cliente)
                .WithMany(c => c.Perfis)
                .HasForeignKey(f => f.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // importante para não deletar perfis ao deletar cliente
        }
    }
}
