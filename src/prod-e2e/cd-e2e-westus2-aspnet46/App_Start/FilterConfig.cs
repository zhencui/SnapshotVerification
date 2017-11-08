using System.Web;
using System.Web.Mvc;

namespace cd_e2e_westus2_aspnet46
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
        }
    }
}
