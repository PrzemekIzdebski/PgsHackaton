using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PgsTwitter.Services;

namespace PgsTwitter.Controllers.Login
{
    public class CheckUserFilter : IAuthorizationFilter
    {
        public static String LoginUrl = "Login";

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipLoginCheckAttribute), false).Any())
            {
                return;
            }
            if (LoginServices.UserName == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { action = "Login", Controller = "Login" }));    
            }
            
        }
    }
}