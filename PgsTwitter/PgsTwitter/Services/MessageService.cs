using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using PgsTwitter.Common;
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

        public ICollection<Tag> GetAllTags()
        {
            var tags = _context.Scan<Tag>();
            return tags.ToList();
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
            var tags = ParseTags(text);
            foreach (var tag in tags)
            {
                AddToTag(tag);
            }
        }

        private void AddToTag(string tagName)
        {
            var tag = _context.Load<Tag>(tagName);
            if (tag == null)
            {
                tag = new Tag()
                    {
                        Name = tagName,
                        Count = 0
                    };
            }
            tag.Count += 1;
            _context.Save(tag);

        }

        private List<string> ParseTags(string text)
        {
            var result = new List<string>();
            var matches = System.Text.RegularExpressions.Regex.Matches(text, "#[a-zA-Z0-9]*");
            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }
            return result;
        }
    }
}