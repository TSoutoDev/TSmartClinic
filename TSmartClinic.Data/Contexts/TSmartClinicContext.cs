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
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new TarefasConfiguration());
            modelBuilder.ApplyConfiguration(new ModuloConfiguration());
            //  modelBuilder.ApplyConfiguration(new TipoDocumentoMap());


        }

    }
}