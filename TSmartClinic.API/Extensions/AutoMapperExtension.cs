using AgendaApp.API.Mapper;
using AutoMapper;

public static class AutoMapperExtension
{
    public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutoMapperConfig).Assembly);
        });

        services.AddSingleton(config.CreateMapper());
        services.AddSingleton(config); // opcional

        return services;
    }
}
