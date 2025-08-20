using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UsuarioClientePerfilConfiguration : IEntityTypeConfiguration<UsuarioClientePerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioClientePerfil> builder)
        {
            builder.ToTable("UsuarioClientePerfil");

            // Chave primária composta
            builder.HasKey(x => new { x.UsuarioId, x.ClienteId, x.PerfilId });

            // Relacionamentos
            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.UsuarioClientePerfil)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.UsuarioClientePerfil)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Perfil)
                .WithMany(p => p.UsuarioClientePerfil)
                .HasForeignKey(x => x.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);

            // Default para ClientePadrao
            builder.Property(x => x.ClientePadrao).HasDefaultValue(false);
        }
    }
}
