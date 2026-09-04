using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UsuarioUnidadePerfilConfiguration : IEntityTypeConfiguration<UsuarioUnidadePerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioUnidadePerfil> builder)
        {
            builder.ToTable("UsuarioUnidadePerfil", "dbo");

            builder.HasKey(x => new { x.UsuarioId, x.UnidadeId, x.PerfilId });

            builder.Property(x => x.UnidadePadrao).IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.UsuarioUnidadePerfil)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unidade)
                .WithMany(x => x.UsuariosUnidadePerfil)
                .HasForeignKey(x => x.UnidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Perfil)
                .WithMany()
                .HasForeignKey(x => x.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}