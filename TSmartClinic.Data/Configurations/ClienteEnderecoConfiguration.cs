using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class ClienteEnderecoConfiguration : IEntityTypeConfiguration<ClienteEndereco>
    {
        public void Configure(EntityTypeBuilder<ClienteEndereco> builder)
        {
            builder.ToTable("ClienteEndereco", "dbo");

            builder.HasKey(x => new
            {
                x.ClienteId,
                x.EnderecoId
            });

            builder.Property(x => x.ClienteId).HasColumnName("ClienteId").IsRequired();
            builder.Property(x => x.EnderecoId).HasColumnName("EnderecoId").IsRequired();
            builder.Property(x => x.Tipo).HasColumnName("Tipo").HasMaxLength(50);

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.ClienteEndereco)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Endereco)
                .WithMany()
                .HasForeignKey(x => x.EnderecoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}