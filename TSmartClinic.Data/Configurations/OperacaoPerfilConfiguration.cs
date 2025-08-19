using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class OperacaoPerfilConfiguration : IEntityTypeConfiguration<OperacaoPerfil>
    {
        public void Configure(EntityTypeBuilder<OperacaoPerfil> builder)
        {

            builder.ToTable("OperacaoPerfil");

            // >>> UMA ÚNICA PK: composta <<<
            builder.HasKey(x => new { x.PerfilId, x.OperacaoId });

            // >>> Ignore o Id herdado da Base para não confundir EF/banco <<<
            builder.Ignore(x => x.Id);

            builder.Property(x => x.PerfilId).HasColumnName("PerfilId");
            builder.Property(x => x.OperacaoId).HasColumnName("OperacaoId");

            builder.HasOne(op => op.Perfil)
                .WithMany(p => p.OperacaoPerfis)
                .HasForeignKey(op => op.PerfilId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(op => op.Operacao)
                .WithMany(o => o.OperacaoPerfis) // ou .WithMany() se a coleção não existir em Operacao
                .HasForeignKey(op => op.OperacaoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
