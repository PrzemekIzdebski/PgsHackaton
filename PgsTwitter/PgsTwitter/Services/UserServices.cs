using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;
using PgsTwitter.Entities;

namespace PgsTwitter.Services
{
    public class UserServices
    {
        private DynamoDBContext context;

        public IEnumerable<User> List()
        {
            return context.Scan<User>();
        } 

        public UserServices(DynamoDBContext context)
        {
            this.context = context;
        }

        public void CreateUserIfNotExists(string userName)
        {
            if (!UserExists(userName))
            {
                CreateUser(userName);
            }
        }

        private void CreateUser( string userName)
        {
            var user = new User()
            {
                Username = userName
            };
            context.Save<User>(user);
        }

        private bool UserExists(string userName)
        {
            var user = context.Load<User>(userName);
            return user != null;
        }


    }
}