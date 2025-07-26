using TSmartClinic.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TSmartClinic.API.Extensions
{
    public static class SqlServerExtension
    {
        public static IServiceCollection AddSqlServerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TSmartClinic");

            services.AddDbContext<TSmartClinicContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptions =>
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,                            // Quantidade de tentativas
                        maxRetryDelay: TimeSpan.FromSeconds(10),    // Tempo entre tentativas
                        errorNumbersToAdd: null                     // Erros adicionais (opcional)
                    )
                )
            );

            return services;
        }
    }
}
