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
            builder.ToTable("Endereco", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(u => u.Id)
                   .IsRequired()
                  .HasColumnName("Id")
                  .ValueGeneratedOnAdd() // diga ao EF que o valor é gerado;
                  .UseIdentityByDefaultColumn(); // mapeia identity do Postgres;
            builder.Property(e => e.Logradouro).HasColumnName("Logradouro").HasMaxLength(300).IsRequired();
            builder.Property(e => e.Numero).HasColumnName("Numero").HasMaxLength(40);
            builder.Property(e => e.Complemento).HasColumnName("Complemento").HasMaxLength(200);
            builder.Property(e => e.Bairro).HasColumnName("Bairro").HasMaxLength(200);
            builder.Property(e => e.Cidade).HasColumnName("Cidade").HasMaxLength(200);
            builder.Property(e => e.Estado).HasColumnName("Estado").HasMaxLength(100);
            builder.Property(e => e.Cep).HasColumnName("CEP").HasMaxLength(10);
            builder.Property(e => e.EstadoId).HasColumnName("EstadoId");
            builder.Property(e => e.MunicipioId).HasColumnName("MunicipioId");
            builder.Property(e => e.MunicipioId).HasColumnName("MunicipioId");

            builder.HasOne(e => e.EstadoNavigation)
                .WithMany()
                .HasForeignKey(e => e.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Municipio)
                .WithMany()
                .HasForeignKey(e => e.MunicipioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
