using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;
using PgsTwitter.Entities;
using PgsTwitter.Models.Users;

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
        
        public User Load(string userName)
        {
            return context.Load<User>(userName);
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


        public void AddToObserved(AddObservedModel addObservedModel)
        {
            var user = context.Load<User>(addObservedModel.ObservingUser);

            user.AddToLiked(addObservedModel.ObservedUser);
            context.Save<User>(user);
        }
    }
}