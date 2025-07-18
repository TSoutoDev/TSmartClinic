using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class ModuloConfiguration : IEntityTypeConfiguration<Modulo>
    {
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            //nome da tabela
            builder.ToTable("Modulo");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomeModulo).HasColumnName("NomeModulo").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Descricao).HasColumnName("Descricao").IsRequired();
            builder.Property(c => c.Ativo).HasColumnName("Ativo").IsRequired();

        }
    }
}
