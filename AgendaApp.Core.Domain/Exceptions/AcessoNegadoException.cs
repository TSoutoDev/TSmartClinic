namespace AgendaApp.Core.Domain.Exceptions
{
    public class AcessoNegadoException : Exception
    {
        public override string Message
          => "Acesso negado. Usuário e/ou senha inválidos.";
    }
}
