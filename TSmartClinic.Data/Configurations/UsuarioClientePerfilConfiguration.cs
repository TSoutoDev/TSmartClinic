using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UsuarioClientePerfilConfiguration : IEntityTypeConfiguration<UsuarioClientePerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioClientePerfil> builder)
        {
            // Nome da tabela (opcional se o nome da entidade for igual)
            builder.ToTable("UsuarioClientePerfil");

            // Chave composta
            builder.HasKey(ee => new { ee.Id, ee.ClienteId, ee.PerfilId });


            builder.Property(c => c.Id).HasColumnName("UsuarioId");
            // Configura coluna ClinicaPadrao (opcional se seguir convenção)
            builder.Property(ee => ee.ClientePadrao)
                   .HasColumnName("ClinicaPadrao")
                   .HasColumnType("bit")
                   .IsRequired()
                   .HasDefaultValue(false); // ou 0
        }
    }
}
