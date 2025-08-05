using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            //nome da tabela
            builder.ToTable("Perfil");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomePerfil).HasColumnName("NomePerfil").HasMaxLength(100).IsRequired();
            builder.Property(c => c.ValidadeDias).HasColumnName("ValidadeDias");
            builder.Property(c => c.ErrosSenha).HasColumnName("ErrosSenha");
            builder.Property(c => c.ResponsavelTecnico).HasColumnName("ResponsavelTecnico");
            builder.Property(c => c.Cliente).HasColumnName("Cliente");
            builder.Property(c => c.Ativo).HasColumnName("Ativo");
            builder.Property(c => c.NichoId).HasColumnName("NichoId").IsRequired();

            //mapeamento do relacionamento (1pN)
            builder.HasOne(f => f.Nicho)
            .WithMany(o => o.Perfis)
            .HasForeignKey(f => f.NichoId);
        }
    }
}
