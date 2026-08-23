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
            builder.ToTable("Municipio", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(u => u.Id)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd() // diga ao EF que o valor é gerado;
            .UseIdentityByDefaultColumn(); // mapeia identity do Postgres;
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
