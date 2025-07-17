using AgendaApp.Core.Domain.Entities;
using AgendaApp.Data.Configurations;
using AgendaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace AgendaApp.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para conexãodo EntityFramework com o banco de dados.
    /// </summary>

    public class AgendaAppContext : DbContext
    {
        public AgendaAppContext(DbContextOptions<AgendaAppContext> options) : base(options) { }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new TarefasConfiguration());
          //  modelBuilder.ApplyConfiguration(new TipoDocumentoMap());


        }

    }
}