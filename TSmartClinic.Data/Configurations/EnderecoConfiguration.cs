using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            //nome da tabela
            builder.ToTable("Endereco");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.Logradouro).HasColumnName("Logradouro").HasMaxLength(300).IsRequired();
            builder.Property(e => e.Numero).HasColumnName("Numero").HasMaxLength(40);
            builder.Property(e => e.Complemento).HasColumnName("Complemento").HasMaxLength(200);
            builder.Property(e => e.Bairro).HasColumnName("Bairro").HasMaxLength(200);
            builder.Property(e => e.Cidade).HasColumnName("Cidade").HasMaxLength(200);
            builder.Property(e => e.Estado).HasColumnName("Estado").HasMaxLength(100);
            builder.Property(e => e.Cep).HasColumnName("CEP").HasMaxLength(10);
        }
    }
}
