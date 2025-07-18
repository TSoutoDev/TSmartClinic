namespace TSmartClinic.Core.Domain.Exceptions
{
    public class TSmartClinicException : Exception
    {
        public int StatusCode { get; set; }
        public string Mensagem {get; set;}
        public TSmartClinicException(int statusCode, string mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }
        public override string Message => base.Message;

    }
}
