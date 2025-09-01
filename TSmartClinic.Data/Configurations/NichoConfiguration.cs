using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class NichoConfiguration : IEntityTypeConfiguration<Nicho>
    {
        public void Configure(EntityTypeBuilder<Nicho> builder)
        {
            //nome da tabela
            builder.ToTable("Nicho");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomeNicho).HasColumnName("NomeNicho").HasMaxLength(200).IsRequired();
            builder.Property(c => c.Ativo).HasColumnName("Ativo").IsRequired();
        }
    }
}
