using AgendaApp.Data.Mappings;
using Microsoft.EntityFrameworkCore;
namespace AgendaApp.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para conexãodo EntityFramework com o banco de dados.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)  { }
        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)

        {
            //mapeamento da string de conexão com o banco de dados
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BdAgendaApp;Integrated Security=True;");
        
}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionar cada classe de mapeamento do projeto
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new TarefasMap());
            modelBuilder.ApplyConfiguration(new DocumentoMap());
            modelBuilder.ApplyConfiguration(new TipoDocumentoMap());
        }
    }
}
