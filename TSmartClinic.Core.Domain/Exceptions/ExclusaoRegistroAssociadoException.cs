namespace TSmartClinic.Core.Domain.Exceptions
{
    public class ExclusaoRegistroAssociadoException : TSmartClinicException
    {
        public ExclusaoRegistroAssociadoException() : base(400, "Não é possível excluir o registro pois existem registros associados")
        {
        }
    }
}
