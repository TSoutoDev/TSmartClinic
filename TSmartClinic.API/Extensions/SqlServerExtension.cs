using TSmartClinic.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TSmartClinic.API.Extensions
{
    public static class SqlServerExtension
    {
        public static IServiceCollection AddSqlServerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BdAgendaApp");

            services.AddDbContext<TSmartClinicContext>(
                options => options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}
