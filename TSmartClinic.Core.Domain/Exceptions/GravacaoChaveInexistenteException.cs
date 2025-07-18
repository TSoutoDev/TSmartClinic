namespace TSmartClinic.Core.Domain.Exceptions
{
    public class GravacaoChaveInexistenteException : TSmartClinicException
    {
        public GravacaoChaveInexistenteException() : base(400, "Não é possível gravar o registro pois está sendo associado a um registro (chave)")
        {
        }
    }
}

