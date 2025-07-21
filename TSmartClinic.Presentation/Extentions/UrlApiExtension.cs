using TSmartClinic.Presentation.Settings;

namespace TSmartClinic.Presentation.Extentions
{
    public static class UrlApiExtension
    {
        public static IServiceCollection AddUrlApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UrlApiSettings>(configuration.GetSection("UrlApiSettings"));

            return services;
        }
    }
}
