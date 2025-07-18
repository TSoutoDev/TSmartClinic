using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            //nome da tabela
            builder.ToTable("Paciente");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomePaciente).HasColumnName("NomePaciente").HasMaxLength(300).IsRequired();
            builder.Property(c => c.DataNascimento).HasColumnName("DataNascimento").HasColumnType("date");
            builder.Property(c => c.CPF).HasColumnName("CPF").HasMaxLength(14);
            builder.Property(c => c.Telefone).HasColumnName("Telefone").HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(100);
            builder.Property(c => c.Observacoes).HasColumnName("Observacoes").HasMaxLength(8000);
            builder.Property(c => c.Ativo).HasColumnName("Ativo").IsRequired();
            builder.Property(c => c.DataCadastro).HasColumnName("DataCadastro").HasColumnType("date");
            builder.Property(c => c.ConvenioId).HasColumnName("ConvenioId");

            //mapeamento do relacionamento (1pN)
            builder.HasOne(p => p.Convenio)
            .WithMany(c => c.Pacientes)
            .HasForeignKey(p => p.ConvenioId);
        }
    }
}
