namespace TSmartClinic.Core.Domain.Exceptions
{
    public class AcessoNegadoException : TSmartClinicException
    {
        public AcessoNegadoException()
            : base(403, "Você não tem permissão para executar esta operação.") { }

        public AcessoNegadoException(string mensagemPersonalizada)
            : base(403, mensagemPersonalizada) { }
    }

    public class NaoAutenticadoException : TSmartClinicException
    {
        public NaoAutenticadoException()
            : base(401, "Sua sessão expirou ou você não está autenticado.") { }

        public NaoAutenticadoException(string mensagemPersonalizada)
            : base(401, mensagemPersonalizada) { }
    }
}
