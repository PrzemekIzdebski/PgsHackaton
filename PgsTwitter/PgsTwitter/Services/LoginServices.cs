using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;
using PgsTwitter.Entities;

namespace PgsTwitter.Services
{
    public static class LoginServices
    {
        private const string UserKey = "user";

        public static void LogIn(string userName, DynamoDBContext context)
        {
            var userService = new UserServices(context);
            userService.CreateUserIfNotExists(userName);

            HttpContext.Current.Session[UserKey] = userName;
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