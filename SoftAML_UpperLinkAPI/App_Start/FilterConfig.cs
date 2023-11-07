using System.Web;
using System.Web.Mvc;

namespace SoftAML_UpperLinkAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
