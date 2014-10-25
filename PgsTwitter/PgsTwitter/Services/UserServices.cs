using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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
            var model = new Observing()
                {
                    ObservedUser = addObservedModel.ObservedUser,
                    ObservingUser = addObservedModel.ObservingUser
                };
            var existing = context.Query<Observing>(addObservedModel.ObservingUser, QueryOperator.Equal, addObservedModel.ObservedUser);
            if (!existing.Any())
            {
                context.Save<Observing>(model);    
            }
        }

        public List<string> GetObserved(string userName)
        {
            var queryResult = context.Query<Observing>(userName);
            return queryResult.Select(observing => observing.ObservedUser).ToList();
        }
    }
}