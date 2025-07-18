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
            builder.HasKey(ee => new { ee.UsuarioId, ee.ClinicaId, ee.PerfilId });
        }
    }
}
