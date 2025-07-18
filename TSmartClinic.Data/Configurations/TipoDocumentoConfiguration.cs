using TSmartClinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TSmartClinic.Data.Configurations
{
    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            //nome da tabela
            builder.ToTable("TipoDocumento");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(50).IsRequired();
            builder.Property(c => c.FlagSituacao).HasColumnName("FlagSituacao").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("DataCriacao").HasColumnType("date");
            builder.Property(c => c.UsuarioCriacao).HasColumnName("UsuarioCriacao").HasMaxLength(100);
            builder.Property(c => c.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("date");
            builder.Property(c => c.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasMaxLength(100);
        }
    }
}
