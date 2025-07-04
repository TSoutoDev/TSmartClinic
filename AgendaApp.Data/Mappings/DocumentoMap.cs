using AgendaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaApp.Data.Mappings
{
    public class DocumentoMap : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            //nome da tabela
            builder.ToTable("Documento");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(100).IsRequired();
            builder.Property(c => c.TextoHtml).HasColumnName("TextoHtml").IsRequired();
            builder.Property(c => c.FlagSituacao).HasColumnName("FlagSituacao").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("DataCriacao").HasColumnType("date");
            builder.Property(c => c.UsuarioCriacao).HasColumnName("UsuarioCriacao").HasMaxLength(100);
            builder.Property(c => c.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("date");
            builder.Property(c => c.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasMaxLength(100);
            builder.Property(c => c.TipoDocumentoId).HasColumnName("TipoDocumentoId").IsRequired();

            //mapeamento do relacionamento (1pN)
            builder.HasOne(d => d.TipoDocumento) //tarefa TEM 1 Categoria
            .WithMany(t => t.Documentos) //categoria TEM MUITAS Tarefas
            .HasForeignKey(t => t.TipoDocumentoId); //Chave estrangeira
        }
    }
}
