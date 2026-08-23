using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class FuncionalidadeConfiguration : IEntityTypeConfiguration<Funcionalidade>
    {
        public void Configure(EntityTypeBuilder<Funcionalidade> builder)
        {
            //nome da tabela
            builder.ToTable("Funcionalidade","dbo");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(u => u.Id)
                   .HasColumnName("Id")
                   .ValueGeneratedOnAdd() // diga ao EF que o valor é gerado;
                   .UseIdentityByDefaultColumn(); // mapeia identity do Postgres;
            builder.Property(c => c.NomeFuncionalidade).HasColumnName("NomeFuncionalidade").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Descricao).HasColumnName("Descricao").HasMaxLength(250);
            builder.Property(c => c.ModuloId).HasColumnName("ModuloId").IsRequired();

            //mapeamento do relacionamento (1pN)
            builder.HasOne(t => t.Modulo)
            .WithMany(c => c.Funcionalidades)
            .HasForeignKey(t => t.ModuloId);
        }
    }
}

