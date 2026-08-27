namespace TSmartClinic.API.Handles
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PermissionModuleAttribute : Attribute
    {
        public string Nome { get; }

        public PermissionModuleAttribute(string nome)
        {
            Nome = nome;
        }
    }
}