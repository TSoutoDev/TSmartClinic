using TSmartClinic.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TSmartClinic.API.Extensions
{
    public static class PostgresExtension
    {
        public static IServiceCollection AddPostgresConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TSmartClinic");

            services.AddDbContext<TSmartClinicContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,                           // Tentativas
                        maxRetryDelay: TimeSpan.FromSeconds(10),   // Intervalo entre tentativas
                        errorCodesToAdd: null                      // Erros extras (opcional)
                    )
                )
            );

            return services;
        }
    }
}
