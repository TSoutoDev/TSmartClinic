using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UnidadeEnderecoConfiguration : IEntityTypeConfiguration<UnidadeEndereco>
    {
        public void Configure(EntityTypeBuilder<UnidadeEndereco> builder)
        {
            builder.ToTable("UnidadeEndereco", "dbo");
            builder.HasKey(x => new { x.UnidadeId, x.EnderecoId });
            builder.Property(x => x.Tipo).HasMaxLength(50);
            builder.HasOne(x => x.Unidade).WithMany(x => x.Enderecos).HasForeignKey(x => x.UnidadeId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Endereco)
                .WithMany()
                .HasForeignKey(x => x.EnderecoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}