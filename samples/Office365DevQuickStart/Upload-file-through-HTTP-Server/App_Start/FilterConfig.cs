using System.Web;
using System.Web.Mvc;

namespace Upload_file_through_HTTP_Server
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
