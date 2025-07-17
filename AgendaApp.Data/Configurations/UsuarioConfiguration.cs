using AgendaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaApp.Data.Configurations;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        // Nome da tabela
        builder.ToTable("Usuario");

        // Supondo que a entidade tenha um campo de chave primária chamado "Id"
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("Id");
        builder.Property(u => u.Login).HasColumnName("Login").HasMaxLength(100);
        builder.Property(u => u.Senha).HasColumnName("Senha").HasMaxLength(510);
        builder.Property(u => u.Nome).HasColumnName("Nome").HasMaxLength(150);
        builder.Property(u => u.LoginInclusao).HasColumnName("LoginInclusao").HasMaxLength(100);
        builder.Property(u => u.DataInclusao).HasColumnName("DataInclusao").HasColumnType("datetime");
        builder.Property(u => u.LoginAlteracao).HasColumnName("LoginAlteracao").HasMaxLength(100);
        builder.Property(u => u.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime");
        builder.Property(u => u.DataBloqueio).HasColumnName("DataBloqueio").HasColumnType("datetime");
        builder.Property(u => u.DataUltimoAcesso).HasColumnName("DataUltimoAcesso").HasColumnType("datetime");
        builder.Property(u => u.DataExpiracaoSenha).HasColumnName("DataExpiracaoSenha").HasColumnType("datetime");
        builder.Property(u => u.Email).HasColumnName("Email").HasMaxLength(255);
        builder.Property(u => u.Celular).HasColumnName("Celular").HasMaxLength(20);
        builder.Property(u => u.Cliente).HasColumnName("Cliente").HasMaxLength(100);
        builder.Property(u => u.TipoUsuario).HasColumnName("TipoUsuario").HasColumnType("char").HasMaxLength(1);
        builder.Property(u => u.Foto).HasColumnName("Foto");
        builder.Property(u => u.FlagBloqueado).HasColumnName("FlagBloqueado").IsRequired();
        builder.Property(u => u.Ativo).HasColumnName("Ativo").IsRequired();
    }
}
