using Microsoft.AspNetCore.Mvc.Rendering;

namespace TSmartClinic.Presentation.Extentions
{
    public static class UtilExtension
    {
        public static List<SelectListItem> InsertBlankItem(this List<SelectListItem> obj)
        {
            obj.Insert(0, new SelectListItem { Text = string.Empty, Value = "0" });

            return obj;
        }
    }
}
