using TSmartClinic.Core.Infra.CrossCutting.Email;
using TSmartClinic.Core.Infra.CrossCutting.Email.FilaEmails;

namespace TSmartClinic.API.Extensions
{
    public static class EmailExtension
    {
        public static IServiceCollection AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Mapeia o bloco "SmtpSettings" do appsettings.json para a classe SmtpSettings
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            // Registra o serviço de envio de e-mails
            services.AddScoped<IEmailService, SmtpEmailService>();

            //Registra a fila (singleton, pois deve ser única na aplicação)
            services.AddSingleton<EmailQueue>();

            //Registra o serviço em background que processa a fila
            services.AddHostedService<EmailBackgroundService>();
            return services;
        }
    }
}
