using TSmartClinic.Core.Infra.CrossCutting.Email;

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

            return services;
        }
    }
}
