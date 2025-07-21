using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Infra.CrossCutting.Providers;

namespace TSmartClinic.Presentation.Extentions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {

            services.AddSingleton<ICriptografiaProvider, CriptografiaProvider>();

            return services;
        }
    }
}
