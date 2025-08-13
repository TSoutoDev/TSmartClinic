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

            // Nome da tabela (opcional se o nome da entidade for igual)
            builder.ToTable("OperacaoPerfil");
            // Chave composta
            builder.HasKey(ee => new { ee.Id, ee.OperacaoId });

            builder.Property(c => c.OperacaoId).HasColumnName("OperacaoId");
            builder.Property(c => c.Id).HasColumnName("PerfilId");

            // Relacionamentos
            builder.HasOne(op => op.Perfil)
            .WithMany(p => p.OperacaoPerfis)
            .HasForeignKey(op => op.Id);

            builder.HasOne(op => op.Operacao)
            .WithMany(o => o.OperacaoPerfis)
            .HasForeignKey(op => op.OperacaoId);
        }
    }
}
