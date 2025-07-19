using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            //nome da tabela
            builder.ToTable("Estado");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.Uf).HasColumnName("Uf").HasMaxLength(2).IsRequired();
            builder.Property(e => e.NomeEstado).HasColumnName("NomeEstado").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Latitude).HasColumnName("Latitude").IsRequired();
            builder.Property(e => e.Longitude).HasColumnName("Longitude").IsRequired();
            builder.Property(e => e.Regiao).HasColumnName("Regiao").IsRequired();

        }
    }
}
