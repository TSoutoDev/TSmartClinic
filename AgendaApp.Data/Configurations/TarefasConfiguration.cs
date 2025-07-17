using AgendaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaApp.Data.Configurations
{
    public class TarefasConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            //nome da tabela
            builder.ToTable("Tarefa");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Data).HasColumnName("Data").HasColumnType("date").IsRequired();
            builder.Property(c => c.Hora).HasColumnName("Hora").HasColumnType("time").IsRequired();
            builder.Property(c => c.Prioridade).HasColumnName("Prioridade").IsRequired();
            builder.Property(c => c.FlagSituacao).HasColumnName("FlagSituacao").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("DataCriacao").HasColumnType("date");
            builder.Property(c => c.UsuarioCriacao).HasColumnName("UsuarioCriacao").HasMaxLength(100);
            builder.Property(c => c.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("date");
            builder.Property(c => c.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasMaxLength(100);
            builder.Property(c => c.CategoriaId).HasColumnName("CategoriaId").IsRequired();
            
            //mapeamento do relacionamento (1pN)
            builder.HasOne(t => t.Categoria) //tarefa TEM 1 Categoria
            .WithMany(c => c.Tarefas) //categoria TEM MUITAS Tarefas
            .HasForeignKey(t => t.CategoriaId); //Chave estrangeira
        }
    }
}
