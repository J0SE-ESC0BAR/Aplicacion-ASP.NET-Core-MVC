using System.Web;
using System.Web.Mvc;

namespace Proyecto_Photos_Adventure_Works
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
