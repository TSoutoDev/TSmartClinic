using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace TSmartClinic.Presentation.Extentions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                  .AddFluentValidationClientsideAdapters()
                  .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
