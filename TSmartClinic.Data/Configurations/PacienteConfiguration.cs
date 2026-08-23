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
            builder.ToTable("Paciente", "dbo");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(u => u.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd() // diga ao EF que o valor é gerado;
            .UseIdentityByDefaultColumn(); // mapeia identity do Postgres;
            builder.Property(c => c.NomePaciente).HasColumnName("NomePaciente").HasMaxLength(300).IsRequired();
            builder.Property(c => c.DataNascimento).HasColumnName("DataNascimento").HasColumnType("date");
            builder.Property(c => c.CPF).HasColumnName("CPF").HasMaxLength(14);
            builder.Property(c => c.Telefone).HasColumnName("Telefone").HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(100);
            builder.Property(c => c.Observacoes).HasColumnName("Observacoes").HasMaxLength(8000);
            builder.Property(c => c.Ativo).HasColumnName("Ativo").IsRequired();
            builder.Property(c => c.DataCadastro).HasColumnName("DataCadastro").HasColumnType("date");
            builder.Property(c => c.ConvenioId).HasColumnName("ConvenioId");
            builder.Property(c => c.Foto).HasColumnName("Foto");
            builder.Property(c => c.ClienteId).HasColumnName("ClienteId").IsRequired(); ;

            //mapeamento do relacionamento (1pN)
            // Paciente -> Convênio
            builder.HasOne(p => p.Convenio)
                .WithMany(c => c.Pacientes)
                .HasForeignKey(p => p.ConvenioId);

            // Paciente -> Cliente/Clínica
            builder.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
