
using Microsoft.EntityFrameworkCore;
using TSmartClinic.Api.Auth.Interfaces.Services;
using TSmartClinic.Api.Auth.Repositories;
using TSmartClinic.Api.Auth.Services;
using TSmartClinic.API.Extensions;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Infra.CrossCutting.Providers;
using TSmartClinic.Core.Infra.Security.Services;
using TSmartClinic.Core.Infra.Security.Settings;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.Api.Auth.Extensions
{
    public static class DependencyInjectionExtension
    {

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSqlServerConfig(configuration); // usa a sua extensão para configurar o DbContext

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IAutenticacaoService, AutenticacaoService>();
            services.AddTransient<ICriptografiaProvider, CriptografiaProvider>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddTransient<IUsuarioClientePerfilService, UsuarioClinicaPerfilService>();
            services.AddTransient<IUsuarioClientePerfilRepository, UsuarioClinicaPerfilRepository>();


            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

            return services;
        }
    }
}
