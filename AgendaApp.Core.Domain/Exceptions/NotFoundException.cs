namespace AgendaApp.Core.Domain.Exceptions
{
    public class NotFoundException : AgendaAppException
    {
        public NotFoundException() : base (404, "O registro não foi encontrado!")
        {
        }
    }
}
