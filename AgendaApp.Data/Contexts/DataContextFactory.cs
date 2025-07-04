using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Data.Contexts
{
    // 1) A fábrica precisa implementar IDesignTimeDbContextFactory<T>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // 2) Cria um builder de opções
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            // 3) Configure a connection string (pode vir do appsettings.json,
            //    mas aqui colocamos "hard‑coded" para exemplificar)
            var connectionString = "Server=localhost;Database=PeopleNET;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);

            // 4) Retorna a instância do seu DataContext com as opções configuradas
            return new DataContext(optionsBuilder.Options);
        }
    }
}
