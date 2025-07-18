using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class ConvenioConfiguration : IEntityTypeConfiguration<Convenio>
    {
        public void Configure(EntityTypeBuilder<Convenio> builder)
        {
            //nome da tabela
            builder.ToTable("Convenio"); 

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomeConvenio).HasColumnName("NomeConvenio").HasMaxLength(300).IsRequired();
            builder.Property(c => c.CNPJ).HasColumnName("CNPJ").HasMaxLength(18);
            builder.Property(c => c.Telefone).HasColumnName("Telefone").HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(200);
            builder.Property(c => c.Ativo).HasColumnName("Ativo");
            builder.Property(c => c.DataCadastro).HasColumnName("DataCadastro").HasColumnType("date");
        }
    }
}
