using TSmartClinic.Core.Domain.Entities;
using TSmartClinic.Data.Configurations;
using TSmartClinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace TSmartClinic.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para conexãodo EntityFramework com o banco de dados.
    /// </summary>

    public class TSmartClinicContext : DbContext
    {
        public TSmartClinicContext(DbContextOptions<TSmartClinicContext> options) : base(options) { }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Convenio> Convenio { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Funcionalidade> Funcionalidade { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Nicho> Nicho { get; set; }
        public DbSet<Operacao> Operacao { get; set; }
        public DbSet<OperacaoPerfil> OperacaoPerfil { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicaConfiguration());
            modelBuilder.ApplyConfiguration(new ConvenioConfiguration());
            modelBuilder.ApplyConfiguration(new TarefasConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionalidadeConfiguration());
            modelBuilder.ApplyConfiguration(new ModuloConfiguration());
            modelBuilder.ApplyConfiguration(new NichoConfiguration());
            modelBuilder.ApplyConfiguration(new OperacaoConfiguration());
            modelBuilder.ApplyConfiguration(new OperacaoPerfilConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilConfiguration());
          //  modelBuilder.ApplyConfiguration(new TipoDocumentoConfiguration());
        }

    }
}