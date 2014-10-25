using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PgsTwitter.Services
{
    public static class LoginServices
    {
        private const string UserKey = "user";

        public static void LogIn(string user)
        {
            HttpContext.Current.Session[UserKey] = user;
        }

        public static void LogOut()
        {
            HttpContext.Current.Session[UserKey] = null;
        }

        public static string UserName
        {
            get { return (string) HttpContext.Current.Session[UserKey]; }
        }



    }
}