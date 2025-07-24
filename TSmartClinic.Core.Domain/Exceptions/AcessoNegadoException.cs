namespace TSmartClinic.Core.Domain.Exceptions
{
    public class AcessoNegadoException : TSmartClinicException
    {
        public AcessoNegadoException()
            : base(403, "Acesso negado. Ou Usuário e/ou senha inválidos.") { }

        public AcessoNegadoException(string mensagemPersonalizada)
            : base(403, mensagemPersonalizada) { }
    }
}
