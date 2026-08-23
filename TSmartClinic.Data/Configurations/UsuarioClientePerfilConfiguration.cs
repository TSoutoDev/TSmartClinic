using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class UsuarioClientePerfilConfiguration : IEntityTypeConfiguration<UsuarioClientePerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioClientePerfil> builder)
        {
            builder.ToTable("UsuarioClientePerfil", "dbo");
      
            // Chave primária composta
            builder.HasKey(x => new { x.UsuarioId, x.ClienteId, x.PerfilId });
            // As PKs de join NUNCA são geradas no banco
            builder.Property(x => x.UsuarioId).ValueGeneratedNever();
            builder.Property(x => x.ClienteId).ValueGeneratedNever();
            builder.Property(x => x.PerfilId).ValueGeneratedNever();

            // Relacionamentos
            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.UsuarioClientePerfil)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.UsuarioClientePerfil)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Perfil)
                .WithMany(p => p.UsuarioClientePerfil)
                .HasForeignKey(x => x.PerfilId)
                .OnDelete(DeleteBehavior.Cascade);


            // Default para ClientePadrao
            builder.Property(x => x.ClientePadrao).HasDefaultValue(false);

            // Único "cliente padrão" por usuário (parcial)
            //builder.HasIndex(x => new { x.UsuarioId })
            // .HasFilter("\"ClientePadrao\" = true")
            // .IsUnique();
        }
    }
}
