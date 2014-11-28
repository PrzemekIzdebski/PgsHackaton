using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using PgsTwitter.Entities;

namespace PgsTwitter.Services
{
    public class MessageService : IMessageService
    {
        private readonly DynamoDBContext _context;
        private readonly UserServices _userServices;


        public MessageService(DynamoDBContext context)
        {
            _context = context;
            _userServices = new UserServices(context);
        }

        public ICollection<Message> GetMessagesBy(string username)
        {
            var msgs = _context.Query<Message>(username);
            return msgs.ToList();
        }

        public ICollection<Message> GetMessagesFor(string username)
        {
            var observedUsers = _userServices.GetObserved(username);
            observedUsers.Add(username);
        }

        public void PostMessage(string username, string text)
        {
            var msg = new Message
            {
                PostedOn = DateTime.Now.Ticks,
                Text = text,
                Username = username
            };
            _context.Save(msg);
        }
    }
}