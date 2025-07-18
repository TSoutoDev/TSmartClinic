namespace TSmartClinic.Core.Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public virtual void Atualizar(object obj) { }
    }
}
