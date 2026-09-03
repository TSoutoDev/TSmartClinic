using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.ToTable("Unidade", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn();
            builder.Property(x => x.PublicId).IsRequired();
            builder.HasIndex(x => x.PublicId).IsUnique();
            builder.Property(x => x.ClienteId).IsRequired();
            builder.Property(x => x.NomeUnidade).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Cnpj).HasColumnName("Cnpj").HasMaxLength(18);
            builder.Property(x => x.Telefone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(200);
            builder.Property(x => x.UnidadePrincipal).IsRequired();
            builder.Property(x => x.Ativo).IsRequired();
            builder.Property(x => x.DataCadastro);

            builder.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}