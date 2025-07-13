using AgendaApp.Data.Entities;
using AgendaApp.Data.Mappings;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());

        }

    }
}