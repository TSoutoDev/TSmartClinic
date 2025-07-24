using Microsoft.Extensions.DependencyInjection;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Infra.CrossCutting.Providers;
using TSmartClinic.Presentation.Services;
using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Extentions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAccessTokenService, AccessTokenService>();
            services.AddTransient<IAutenticacaoService, AutenticacaoService>();
            services.AddSingleton<ICriptografiaProvider, CriptografiaProvider>();
            services.AddTransient<IEmpresaAtivaService, EmpresaAtivaService>();
            services.AddTransient<IUsuarioService, UsuarioService>();


            return services;
        }
    }
}
