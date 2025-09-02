using TSmartClinic.API.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Infra.CrossCutting.Providers;
using TSmartClinic.Presentation.Services;
using TSmartClinic.Presentation.Services.Interfaces;

namespace TSmartClinic.Presentation.Extentions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // Registro do IHttpContextAccessor
            services.AddHttpContextAccessor();

            services.AddTransient<IAccessTokenService, AccessTokenService>();
            services.AddTransient<IAutenticacaoService, AutenticacaoService>();
            services.AddSingleton<ICriptografiaProvider, CriptografiaProvider>();
            services.AddTransient<IEmpresaAtivaService, EmpresaAtivaService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IPerfilService, PerfilService>();
            services.AddTransient<INichoService, NichoService>();
            services.AddTransient<IUsuarioLogadoService, UsuarioLogadoService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IPerfilPermissaoService, PerfilPermissaoService>();

            return services;
        }
    }
}
