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


        public MessageService(DynamoDBContext context)
        {
            _context = context;
        }

        public ICollection<Message> GetMessages(string username)
        {
            var msgs = _context.Query<Message>(username);
            return msgs.ToList();
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