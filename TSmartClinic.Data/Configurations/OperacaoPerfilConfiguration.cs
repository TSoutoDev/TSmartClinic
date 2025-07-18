using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder.HasKey(ee => new { ee.PerfilId, ee.OperacaoId });

        }
    }
}
