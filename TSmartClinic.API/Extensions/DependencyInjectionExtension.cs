using TSmartClinic.API.Repositories;
using TSmartClinic.API.Services;
using TSmartClinic.Core.Domain.Interfaces.Providers;
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Core.Domain.Interfaces.Services;
using TSmartClinic.Core.Domain.Service;
using TSmartClinic.Core.Infra.CrossCutting.Providers;
using TSmartClinic.Data.Repositories;

namespace TSmartClinic.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //Servicos
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<ICategoriaService, CategoriaService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IPacienteService, PacienteService>();
            services.AddTransient<IPerfilService, PerfilService>();

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICriptografiaProvider, CriptografiaProvider>();
           
            // services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
