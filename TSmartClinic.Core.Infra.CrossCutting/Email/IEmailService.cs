using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSmartClinic.Core.Infra.CrossCutting.Email
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string destinatario, string assunto, string corpoHtml);
    }
}
