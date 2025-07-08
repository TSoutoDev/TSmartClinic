using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Core.Domain.Interfaces.Services;
using AgendaApp.Core.Domain.Service;
using AgendaApp.Data.Repositories;

namespace AgendaApp.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //Servicos
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            //Repositorios
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            services.AddTransient<IUnitOfWork, UnitOfWork>();
           // services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
