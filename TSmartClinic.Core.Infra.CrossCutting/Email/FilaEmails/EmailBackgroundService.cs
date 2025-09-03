using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TSmartClinic.Core.Infra.CrossCutting.Email.FilaEmails
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly EmailQueue _emailQueue;
        private readonly ILogger<EmailBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EmailBackgroundService(
            EmailQueue emailQueue,
            ILogger<EmailBackgroundService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _emailQueue = emailQueue;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var email = _emailQueue.Dequeue();

                if (email != null)
                {
                    try
                    {
                        // cria um escopo a cada envio
                        using var scope = _serviceScopeFactory.CreateScope();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        await emailService.EnviarEmailAsync(
                            email.Value.destinatario,
                            email.Value.assunto,
                            email.Value.corpoHtml
                        );

                        _logger.LogInformation("E-mail enviado para {Destinatario}", email.Value.destinatario);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao enviar e-mail para {Destinatario}", email.Value.destinatario);
                    }
                }
                else
                {
                    await Task.Delay(1000, stoppingToken); // espera 1s antes de checar novamente
                }
            }
        }
    }
}
