using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UsuarioClinicaPerfilConfiguration : IEntityTypeConfiguration<UsuarioClinicaPerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioClinicaPerfil> builder)
        {
            // Nome da tabela (opcional se o nome da entidade for igual)
            builder.ToTable("UsuarioClinicaPerfil");

            // Chave composta
            builder.HasKey(ee => new { ee.Id, ee.ClinicaId, ee.PerfilId });


            builder.Property(c => c.Id).HasColumnName("UsuarioId");
            // Configura coluna ClinicaPadrao (opcional se seguir convenção)
            builder.Property(ee => ee.ClinicaPadrao)
                   .HasColumnName("ClinicaPadrao")
                   .HasColumnType("bit")
                   .IsRequired()
                   .HasDefaultValue(false); // ou 0
        }
    }
}
