namespace TSmartClinic.Core.Domain.Exceptions
{
    public class NotFoundException : TSmartClinicException
    {
        public NotFoundException() : base (404, "O registro não foi encontrado!")
        {
        }
    }
}
