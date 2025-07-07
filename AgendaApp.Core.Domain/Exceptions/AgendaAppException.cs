namespace AgendaApp.Core.Domain.Exceptions
{
    public class AgendaAppException : Exception
    {
        public int StatusCode { get; set; }
        public string Mensagem {get; set;}
        public AgendaAppException(int statusCode, string mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }
        public override string Message => base.Message;

    }
}
