using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            //nome da tabela
            builder.ToTable("Municipio");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.NomeMunicipio).HasColumnName("NomeMunicipio").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Latitude).HasColumnName("Latitude").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Longitude).HasColumnName("Longitude").IsRequired();
            builder.Property(e => e.Capital).HasColumnName("Capital").IsRequired();
            builder.Property(e => e.Codigo_uf).HasColumnName("Codigo_uf").IsRequired();
            builder.Property(e => e.Siafi_id).HasColumnName("Siafi_id").IsRequired();
            builder.Property(e => e.Ddd).HasColumnName("Ddd").IsRequired();
            builder.Property(e => e.Fuso_horario).HasColumnName("Fuso_horario").HasMaxLength(50).IsRequired();
        }
    }
}
