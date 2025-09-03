namespace TSmartClinic.Core.Infra.CrossCutting.Email.FilaEmails
{
    /// <summary>
    /// Criar a fila de e-mails
    /// </summary>
    public class EmailQueue
    {
        private readonly Queue<(string Destinatario, string Assunto, string CorpoHtml)> _queue = new();
        public void Enqueue(string destinatario, string assunto, string corpoHtml)
        {
            lock (_queue)
            {
                _queue.Enqueue((destinatario, assunto, corpoHtml));
            }
        }

        public (string destinatario, string assunto, string corpoHtml)? Dequeue()
        {
            lock (_queue) 
            {
                return _queue.Count > 0 ? _queue.Dequeue() : null;
            }
        }
    }
}
