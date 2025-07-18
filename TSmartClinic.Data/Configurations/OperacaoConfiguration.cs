using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class OperacaoConfiguration : IEntityTypeConfiguration<Operacao>
    {
        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            //nome da tabela
            builder.ToTable("Operacao");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomeOperacao).HasColumnName("NomeOperacao").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Descricao).HasColumnName("Descricao").HasMaxLength(255);
            builder.Property(c => c.FuncionalidadeId).HasColumnName("FuncionalidadeId").IsRequired();

            //mapeamento do relacionamento (1pN)
            builder.HasOne(f => f.Funcionalidade)
            .WithMany(o => o.Operacoes)
            .HasForeignKey(f => f.FuncionalidadeId);
        }
    }
}
