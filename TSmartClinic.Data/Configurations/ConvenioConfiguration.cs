using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class ConvenioConfiguration : IEntityTypeConfiguration<Convenio>
    {
        public void Configure(EntityTypeBuilder<Convenio> builder)
        {
            builder.ToTable("Convenio", "dbo");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd().UseIdentityByDefaultColumn();
            builder.Property(c => c.PublicId).HasColumnName("PublicId").IsRequired();
            builder.HasIndex(c => c.PublicId).IsUnique();
            builder.Property(c => c.NomeConvenio).HasColumnName("NomeConvenio").HasMaxLength(300).IsRequired();
            builder.Property(c => c.CNPJ).HasColumnName("CNPJ").HasMaxLength(18);
            builder.Property(c => c.Telefone).HasColumnName("Telefone").HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(200);
            builder.Property(c => c.Ativo).HasColumnName("Ativo");
            builder.Property(c => c.DataCadastro).HasColumnName("DataCadastro").HasColumnType("date");
            builder.Property(c => c.ClienteId).HasColumnName("ClienteId").IsRequired();

            builder.HasOne(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}