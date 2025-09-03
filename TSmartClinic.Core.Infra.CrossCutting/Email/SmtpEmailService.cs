using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace TSmartClinic.Core.Infra.CrossCutting.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public SmtpEmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task EnviarEmailAsync(string destinatario, string assunto, string corpoHtml)
        {
            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                // ESSENCIAL: nunca usar credenciais padrão
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 1000 * 30 // 30s (opcional)
            };

            using var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.From, "Sistema de Cadastro"),
                Subject = assunto,
                Body = corpoHtml,
                IsBodyHtml = true
            };
            mail.To.Add(destinatario);

            try
            {
                await client.SendMailAsync(mail);
            }
            catch (SmtpException ex)
            {
                // LOGUE a exceção para entender se foi bloqueio do Gmail, credenciais, etc.
                // ex.StatusCode pode ajudar (por ex: GeneralFailure, ClientNotPermitted)
                throw; // ou registre no seu logger e rethrow conforme a sua política
            }
        }
    }
}
