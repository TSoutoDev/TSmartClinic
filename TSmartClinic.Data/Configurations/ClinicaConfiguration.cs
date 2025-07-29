using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations
{
    public class ClinicaConfiguration : IEntityTypeConfiguration<Clinica>
    {
        public void Configure(EntityTypeBuilder<Clinica> builder)
        {
            //nome da tabela
            builder.ToTable("Clinica");

            //definindo o campo 'chave primária'
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.NomeClinica).HasColumnName("NomeClinica").HasMaxLength(300).IsRequired();
            builder.Property(c => c.RazaoSocial).HasColumnName("RazaoSocial").HasMaxLength(300);
            builder.Property(c => c.CNPJ).HasColumnName("CNPJ").HasMaxLength(18);
            builder.Property(c => c.Telefone).HasColumnName("Telefone").HasMaxLength(18);
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(200);
            builder.Property(c => c.Ativo).HasColumnName("Ativo").IsRequired();
            builder.Property(c => c.DataCadastro).HasColumnName("DataCadastro").HasColumnType("date");
            builder.Property(c => c.NichoId).HasColumnName("NichoId").IsRequired();
            

            //mapeamento do relacionamento (1pN)
            builder.HasOne(c => c.Nicho) //tarefa TEM 1 Categoria
            .WithMany(n => n.Clinicas) //categoria TEM MUITAS Tarefas
            .HasForeignKey(c => c.NichoId); //Chave estrangeira
        }
    }
}
