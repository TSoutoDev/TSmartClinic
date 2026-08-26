using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class PacienteEnderecoConfiguration : IEntityTypeConfiguration<PacienteEndereco>
    {
        public void Configure(EntityTypeBuilder<PacienteEndereco> builder)
        {
            builder.ToTable("PacienteEndereco", "dbo");

            builder.HasKey(x => new
            {
                x.PacienteId,
                x.EnderecoId
            });

            builder.Property(x => x.PacienteId).HasColumnName("PacienteId").IsRequired();
            builder.Property(x => x.EnderecoId).HasColumnName("EnderecoId").IsRequired();
            builder.Property(x => x.Tipo).HasColumnName("Tipo").HasMaxLength(50);

            builder.HasOne(x => x.Paciente)
                .WithMany(p => p.PacienteEnderecos)
                .HasForeignKey(x => x.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Endereco)
                .WithMany()
                .HasForeignKey(x => x.EnderecoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}