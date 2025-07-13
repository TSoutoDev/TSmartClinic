using AgendaApp.API.Repositories;
using AgendaApp.API.Services;
using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Interfaces.Services;
using AgendaApp.Core.Domain.Service;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Repositories;

namespace AgendaApp.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //Servicos
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<ICategoriaService, CategoriaService>();

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
           // services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
