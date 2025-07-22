using Microsoft.OpenApi.Models;

namespace TSmartClinic.Api.TSmartClinic.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "TSmartClinicp - Tsouto",
                    Description = "API TSmartClinic",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Tsouto Informática",
                        Email = "tsouto@tsmartClinic.com.br",
                        Url = new Uri("http://www.tsmartClinic.com.br")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no campo abaixo: Bearer {seu token}"
                });

                options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Cookie",
                    In = ParameterLocation.Header,
                    Description = "Autenticação baseada em cookie"
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}

