namespace TSmartClinic.Presentation.Models
{
    public abstract class BaseViewModel 
    {
        public int? Id { get; set; }
        public Guid? PublicId { get; set; }
    }
}
