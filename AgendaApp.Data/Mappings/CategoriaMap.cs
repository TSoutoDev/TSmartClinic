using AgendaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaApp.Data.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //nome da tabela
            builder.ToTable("Categoria");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Descricao).HasColumnName("Descricao").HasMaxLength(50).IsRequired();
            builder.Property(c => c.FlagSituacao).HasColumnName("FlagSituacao").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("DataCriacao").HasColumnType("date");
            builder.Property(c => c.UsuarioCriacao).HasColumnName("UsuarioCriacao").HasMaxLength(100);
            builder.Property(c => c.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("date");
            builder.Property(c => c.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasMaxLength(100);
           
        }
    }
}
