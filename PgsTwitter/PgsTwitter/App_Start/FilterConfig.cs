using System.Web;
using System.Web.Mvc;
using PgsTwitter.Controllers;
using PgsTwitter.Controllers.Login;

namespace PgsTwitter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CheckUserFilter());
        }
    }
}