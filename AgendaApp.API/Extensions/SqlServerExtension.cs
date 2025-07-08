using AgendaApp.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.API.Extensions
{
    public static class SqlServerExtension
    {
        public static IServiceCollection AddSqlServerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BdAgendaApp");

            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}
