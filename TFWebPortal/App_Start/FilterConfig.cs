using System.Web;
using System.Web.Mvc;
using TransForce.Web.Portal.App_Start;

namespace TransForce.Web.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
