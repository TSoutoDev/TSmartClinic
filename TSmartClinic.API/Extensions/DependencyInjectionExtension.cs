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
            services.AddTransient<INichoService, NichoService>();
            services.AddTransient<IPacienteService, PacienteService>();
            services.AddTransient<IPerfilService, PerfilService>();
            services.AddTransient<IUsuarioLogadoService, UsuarioLogadoService>();
            

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<INichoRepository, NichoRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICriptografiaProvider, CriptografiaProvider>();


            // Novo: Acesso ao HttpContext para ler claims do token
            services.AddHttpContextAccessor();

            // services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
