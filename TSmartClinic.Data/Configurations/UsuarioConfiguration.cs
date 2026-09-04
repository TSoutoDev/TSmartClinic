using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSmartClinic.Core.Domain.Entities;

namespace TSmartClinic.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario", "dbo");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .UseIdentityByDefaultColumn();

        builder.Property(x => x.PublicId).HasColumnName("PublicId").IsRequired();
        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.Property(u => u.Senha).HasColumnName("Senha").HasMaxLength(510);
        builder.Property(u => u.Nome).HasColumnName("Nome").HasMaxLength(150);
        builder.Property(u => u.LoginInclusao).HasColumnName("LoginInclusao").HasMaxLength(100);
        builder.Property(u => u.DataInclusao).HasColumnName("DataInclusao").HasColumnType("timestamp with time zone");
        builder.Property(u => u.LoginAlteracao).HasColumnName("LoginAlteracao").HasMaxLength(100);
        builder.Property(u => u.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("timestamp with time zone");
        builder.Property(u => u.DataBloqueio).HasColumnName("DataBloqueio").HasColumnType("timestamp with time zone");
        builder.Property(u => u.DataUltimoAcesso).HasColumnName("DataUltimoAcesso").HasColumnType("timestamp with time zone");
        builder.Property(u => u.DataExpiracaoSenha).HasColumnName("DataExpiracaoSenha").HasColumnType("timestamp with time zone");
        builder.Property(u => u.Email).HasColumnName("Email").HasMaxLength(255);
        builder.Property(u => u.Celular).HasColumnName("Celular").HasMaxLength(20);
        builder.Property(u => u.TipoUsuario).HasColumnName("TipoUsuario").HasColumnType("char").HasMaxLength(1);
        builder.Property(u => u.Foto).HasColumnName("Foto");
        builder.Property(u => u.FlagBloqueado).HasColumnName("FlagBloqueado").IsRequired();
        builder.Property(u => u.Ativo).HasColumnName("Ativo").IsRequired();
        builder.Property(u => u.PrimeiroAcesso).HasColumnName("PrimeiroAcesso");
        builder.Property(u => u.ClienteId).HasColumnName("ClienteId").IsRequired();

        // Legado Cliente -> Usuario
        // Manter enquanto Usuario.ClienteId ainda existir.
        builder.HasOne(u => u.Cliente)
            .WithMany(c => c.Usuarios)
            .HasForeignKey(u => u.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}