using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webservice
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute() { View = "Error" });
        }
    }
}
