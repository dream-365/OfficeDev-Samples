using System.Web;
using System.Web.Mvc;

namespace Secure_web_API_with_ADAL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
